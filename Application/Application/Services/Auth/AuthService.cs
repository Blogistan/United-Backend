using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Constants;
using Application.Features.Auth.Rules;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Mailing;
using Core.Persistence.Paging;
using Core.Security.EmailAuthenticator;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator;
using Domain.Entities;
using Google.Apis.Auth;
using Infrastructure.Constants;
using Infrastructure.Dtos;
using Infrastructure.Dtos.Facebook;
using Infrastructure.Dtos.Github;
using Infrastructure.Dtos.Twitter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using OAuth;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ITokenHelper tokenHelper;
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly ISiteUserRepository siteUserRepository;
        private readonly IEmailAuthenticatorRepository emailAuthenticatorRepository;
        private readonly IUserOperationClaimRepository userOperationClaimRepository;
        private readonly IMailService mailService;
        private IOtpAuthenticatorHelper otpAuthenticatorHelper;
        private readonly IEmailAuthenticatorHelper emailAuthenticatorHelper;
        private readonly IOtpAuthenticatorRepository otpAuthenticatorRepository;
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;
        private readonly IUserLoginRepository userLoginRepository;
        private readonly AuthBussinessRules authBussinessRules;

        public AuthService(ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository, ISiteUserRepository siteUserRepository, IEmailAuthenticatorRepository emailAuthenticatorRepository, IUserOperationClaimRepository userOperationClaimRepository, IMailService mailService,
            IOtpAuthenticatorHelper otpAuthenticatorHelper,
            IEmailAuthenticatorHelper emailAuthenticatorHelper,
            IOtpAuthenticatorRepository otpAuthenticatorRepository, HttpClient httpClient, IConfiguration configuration, IUserLoginRepository userLoginRepository, AuthBussinessRules authBussinessRules)
        {
            this.tokenHelper = tokenHelper;
            this.mailService = mailService;
            this.refreshTokenRepository = refreshTokenRepository;
            this.siteUserRepository = siteUserRepository;
            this.emailAuthenticatorRepository = emailAuthenticatorRepository;
            this.userOperationClaimRepository = userOperationClaimRepository;
            this.otpAuthenticatorHelper = otpAuthenticatorHelper;
            this.otpAuthenticatorRepository = otpAuthenticatorRepository;
            this.emailAuthenticatorHelper = emailAuthenticatorHelper;
            this.httpClient = httpClient;
            this.configuration = configuration;
            this.userLoginRepository = userLoginRepository;
            this.authBussinessRules = authBussinessRules;
        }



        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
        {
            RefreshToken addedRefreshToken = await refreshTokenRepository.AddAsync(refreshToken);

            return addedRefreshToken;
        }

        public Task<string> ConvertOtpKeyToString(byte[] secretBtyes)
        {
            return otpAuthenticatorHelper.ConvertSecretKeyToString(secretBtyes);
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            IPaginate<UserOperationClaim> paginate = await userOperationClaimRepository.GetListAsync(x => x.UserId == user.Id, include: x => x.Include(inc => inc.OperationClaim));

            List<OperationClaim> userClaims = paginate.Items.Select(x => new OperationClaim { Id = x.Id, Name = x.OperationClaim.Name }).ToList();

            AccessToken accessToken = tokenHelper.CreateToken(user, userClaims);
            return accessToken;

        }

        public async Task<EmailAuthenticator> CreateEmailAutenticator(User user)
        {
            return new EmailAuthenticator
            {
                UserId = user.Id,
                ActivationKey = await emailAuthenticatorHelper.CreateEmailActivationKey(),
                IsVerified = false
            };
        }

        public RefreshToken CreateRefreshToken(User user, string IpAddress)
        {
            RefreshToken refreshToken = tokenHelper.CreateRefreshToken(user, IpAddress);

            return refreshToken;
        }

        public async Task DeleteOldActiveRefreshTokens(User user)
        {
            ICollection<RefreshToken> oldActiveTokens = await refreshTokenRepository.GetAllOldActiveRefreshTokenAsync(user, tokenHelper.RefreshTokenTTLOption);

            await refreshTokenRepository.DeleteRangeAsync(oldActiveTokens.ToList());
        }

        public async Task RevokeDescendantRefreshTokens(RefreshToken token, string IpAddress, string reason)
        {
            RefreshToken childRefreshToken = (await refreshTokenRepository.GetAsync(rt => token.ReplacedByToken == rt.Token))!;
            if (childRefreshToken == null) throw new BusinessException(AuthBusinessMessage.CouldntFindChildToken);


            if (childRefreshToken.Revoked == null) await RevokeRefreshToken(childRefreshToken, IpAddress, reason, null);
            else await RevokeDescendantRefreshTokens(childRefreshToken, IpAddress, reason);
        }

        public async Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress)
        {
            RefreshToken newToken = tokenHelper.CreateRefreshToken(user, ipAddress);
            await RevokeRefreshToken(refreshToken, ipAddress, "New Refresh Token Created. ", newToken.Token);
            await AddRefreshToken(newToken);
            return newToken;
        }

        public async Task SendAuthenticatorCode(User user)
        {
            switch (user.AuthenticatorType)
            {

                case Core.Security.Enums.AuthenticatorType.Email:
                    await SendAuthenticatorCodeWithEmail(user);
                    break;
            }
        }
        //This method could be updated.
        private async Task SendAuthenticatorCodeWithEmail(User user)
        {
            EmailAuthenticator emailAuthenticator = await emailAuthenticatorRepository.GetAsync(x => x.UserId == user.Id);

            string authCode = await emailAuthenticatorHelper.CreateEmailActivationCode();
            emailAuthenticator.ActivationKey = authCode;

            await emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);


            List<MailboxAddress> mailboxAddresses = new()
            {
                new MailboxAddress(Encoding.UTF8,user.FirstName,user.Email)
            };

            Mail mail = new()
            {
                ToList = mailboxAddresses,
                Subject = AuthBusinessMessage.AuthenticatorCodeSubject,
                TextBody = AuthBusinessMessage.AuthenticatorCodeTextBody(authCode)
            };

            await mailService.SendEmailAsync(mail);


        }

        public async Task VerifyAuthenticatorCode(User user, string code)
        {
            switch (user.AuthenticatorType)
            {

                case Core.Security.Enums.AuthenticatorType.Email:
                    await VerifyEmailAuthenticatorCode(user, code);
                    break;
                case Core.Security.Enums.AuthenticatorType.Otp:
                    await VerifyEmailOtpAuthenticatorCode(user, code);
                    break;

            }
        }

        public async Task RevokeRefreshToken(RefreshToken refreshToken, string IpAddress, string reason, string? replacedByToken)
        {
            refreshToken.RevokedByIp = IpAddress;
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.ReasonRevoked = reason;
            refreshToken.ReplacedByToken = replacedByToken;
            await refreshTokenRepository.UpdateAsync(refreshToken);
        }
        private async Task VerifyEmailAuthenticatorCode(User user, string code)
        {
            EmailAuthenticator emailAuthenticator = await emailAuthenticatorRepository.GetAsync(x => x.UserId == user.Id);

            if (emailAuthenticator.ActivationKey != code)
                await Task.FromException(new BusinessException(AuthBusinessMessage.InvalidAuthenticatorCode));
            await emailAuthenticatorRepository.UpdateAsync(emailAuthenticator);
        }
        private async Task VerifyEmailOtpAuthenticatorCode(User user, string codeToVerify)
        {
            OtpAuthenticator otpAuthenticator = await otpAuthenticatorRepository.GetAsync(x => x.UserId == user.Id);
            bool result = await otpAuthenticatorHelper.VerifyCode(otpAuthenticator.SecretKey, codeToVerify);
            if (!result)
                throw new BusinessException(AuthBusinessMessage.InvalidAuthenticatorCode); ;

        }

        public async Task<OtpAuthenticator> CreateOtpAuthenticator(User user) => new()
        {
            UserId = user.Id,
            SecretKey = await otpAuthenticatorHelper.GenerateSecretKey(),
            IsVerified = false,
        };

        public async Task<LoginResponseBase> CreateUserExternalAsync(User user, string email, string name, string surname, string picture, string ipAdress, LoginProviderType loginProviderType, string providerKey)
        {
            bool result = user != null;

            AccessToken accessToken = new();
            RefreshToken refreshToken = new();

            LoginResponse loginResponse = new();

            if (user == null)
            {

                User createUser = new User()
                {
                    FirstName = name,
                    LastName = surname,
                    Email = email,
                    PasswordHash = new byte[0],
                    PasswordSalt = new byte[0],
                    AuthenticatorType = AuthenticatorType.None,
                    IsActive = true
                };

                SiteUser siteUser = new()
                {
                    IsVerified = false,
                    ProfileImageUrl = picture,
                    User = createUser
                };

                var createdSiteUser = await siteUserRepository.AddAsync(siteUser);
                var createdUser = await siteUserRepository.GetAsync(x => x.Id == createdSiteUser.Id, include: x => x.Include(x => x.User));
                await userOperationClaimRepository.AddAsync(new UserOperationClaim { UserId = createdUser.User.Id, OperationClaimId = 3 });
                accessToken = await CreateAccessToken(createdUser.User);
                refreshToken = CreateRefreshToken(createdUser.User, ipAdress);

                await AddRefreshToken(refreshToken);

                loginResponse.RefreshToken = refreshToken;
                loginResponse.AccessToken = accessToken;

                switch (loginProviderType)
                {
                    case LoginProviderType.Google:
                        await userLoginRepository.AddAsync(new UserLogin("GOOGLE", providerKey, "GOOGLE", createdUser.Id)); break;
                    case LoginProviderType.Facebook:
                        await userLoginRepository.AddAsync(new UserLogin("FACEBOOK", providerKey, "FACEBOOK", createdUser.Id)); break;
                    case LoginProviderType.Twitter:
                        await userLoginRepository.AddAsync(new UserLogin("TWITTER", providerKey, "TWITTER", createdUser.Id)); break;
                    case LoginProviderType.Github:
                        await userLoginRepository.AddAsync(new UserLogin("GITHUB", providerKey, "GITHUB", createdUser.Id)); break;

                }
            }
            else
            {
                await authBussinessRules.IsUserActive(user.Id);

                await authBussinessRules.IsUserTimeOut(user.Id);

                accessToken = await CreateAccessToken(user);
                refreshToken = CreateRefreshToken(user, ipAdress);
                await AddRefreshToken(refreshToken);

                loginResponse.RefreshToken = refreshToken;
                loginResponse.AccessToken = accessToken;

                switch (loginProviderType)
                {
                    case LoginProviderType.Google:
                        await userLoginRepository.AddAsync(new UserLogin("GOOGLE", providerKey, "GOOGLE", user.Id)); break;
                    case LoginProviderType.Facebook:
                        await userLoginRepository.AddAsync(new UserLogin("FACEBOOK", providerKey, "FACEBOOK", user.Id)); break;
                    case LoginProviderType.Twitter:
                        await userLoginRepository.AddAsync(new UserLogin("TWITTER", providerKey, "TWITTER", user.Id)); break;
                    case LoginProviderType.Github:
                        await userLoginRepository.AddAsync(new UserLogin("GITHUB", providerKey, "GITHUB", user.Id)); break;

                }
            }



            return loginResponse;
        }
        public async Task<GoogleJsonWebSignature.Payload> GoogleSignIn(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { configuration["Authentication:Google:client_id"] }
            };

            return await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

        }

        public async Task<FacebookUserInfoResponse> FacebookSignIn(string authToken)
        {
            string accesTokenResponse = await httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={configuration["Authentication:Facebook:AppId"]}&client_secret={configuration["Authentication:Facebook:AppSecret"]}&grant_type=client_credentials");

            FacebookAccessTokenResponse? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accesTokenResponse);

            string userAccessTokenValidation = await httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

            FacebookUserAccessTokenValidation? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

            FacebookUserInfoResponse? userInfoResponse = new();

            if (validation.Data.IsValid != false)
            {
                string userInfoRespons = await httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

                userInfoResponse = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoRespons);
            }
            else
            {
                await Task.FromException(new BusinessException(AuthBusinessMessage.ExternalLoginCredentialsWrong));
            }
            return userInfoResponse;

        }
        public async Task<TwitterLoginLinkResponse> GetTwitterLoginUrl()
        {
            string consumerKey = configuration["Authentication:Twitter:ConsumerAPIKey"];
            string consumerSecret = configuration["Authentication:Twitter:ConsumerSecret"];

            var oauth = new OAuthRequest
            {
                Method = "POST",
                Type = OAuthRequestType.RequestToken,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                RequestUrl = ExternalAPIUrls.TwitterRequestToken,
                Version = "1.0"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, oauth.RequestUrl);
            request.Headers.Add("Authorization", oauth.GetAuthorizationHeader());

            var handler = new HttpClientHandler();

            using (HttpClient httpClient = new HttpClient(handler))
            {
                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var oauthParameters = ParseOAuthResponse(responseContent);

                    string oauthToken = oauthParameters["oauth_token"];
                    string oauthTokenSecret = oauthParameters["oauth_token_secret"];
                    string oauthCallbackConfirmed = oauthParameters["oauth_callback_confirmed"];

                    return new TwitterLoginLinkResponse { LoginURL = $"https://api.twitter.com/oauth/authorize?oauth_token={oauthToken}&oauth_token_secret={oauthTokenSecret}&oauth_callback_confirmed={oauthCallbackConfirmed}" };
                }
                else
                {
                    throw new BusinessException("Error Code: " + response.StatusCode);
                }
            }
        }

        private Dictionary<string, string> ParseOAuthResponse(string response)
        {
            Dictionary<string, string> oauthParameters = new Dictionary<string, string>();
            string[] parameters = response.Split('&');

            foreach (var param in parameters)
            {
                string[] keyValue = param.Split('=');
                if (keyValue.Length == 2)
                {
                    oauthParameters[keyValue[0]] = keyValue[1];
                }
            }

            return oauthParameters;
        }
        public async Task<OAuthResponse> TwitterSignIn(OAuthCredentials oAuthCredentials)
        {

            string consumerKey = configuration["Authentication:Twitter:ConsumerAPIKey"];

            var postData = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("oauth_token", oAuthCredentials.Oauth_token),
            new KeyValuePair<string, string>("oauth_verifier", oAuthCredentials.Oauth_verifier)
             });

            OAuthResponse oAuthResponse = new();
            oAuthResponse.Cookies = new Dictionary<string, string>();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + consumerKey);

                HttpResponseMessage response = await httpClient.PostAsync(ExternalAPIUrls.TwitterAccessToken, postData);

                List<string> cookieNames = new List<string> { "guest_id", "_twitter_sess", "guest_id_ads", "guest_id_marketing", "personalization_id" };

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    var parseResponse = System.Web.HttpUtility.ParseQueryString(responseContent);

                    var cookies = response.Headers.GetValues("Set-Cookie");


                    foreach (var cookie in cookies)
                    {
                        string[] cookieParts = cookie.Split(";");
                        foreach (var searched in cookieNames)
                        {
                            foreach (var item in cookieParts)
                            {
                                string[] keyValue = item.Split("=");
                                if (keyValue[0] == searched)
                                {

                                    if (keyValue.Length == 2)
                                    {
                                        oAuthResponse.Cookies.Add(searched, keyValue[1].Trim());
                                    }
                                }
                            }
                        }
                    }

                    oAuthResponse.Oauth_token_secret = parseResponse["oauth_token_secret"];
                    oAuthResponse.Oauth_token = parseResponse["oauth_token"];

                }
                else
                {
                    await Task.FromException(new BusinessException($"{AuthBusinessMessage.ExternalLoginCredentialsWrong} : Error Code: {response.StatusCode}"));
                }

                return oAuthResponse;
            }

        }
        public async Task<TwitterUserInfo> GetTwitterUserInfo(OAuthResponse oAuthResponse)
        {
            TwitterUserInfo twitterUserInfo = new();
            string consumerKey = configuration["Authentication:Twitter:ConsumerAPIKey"];
            string consumerSecret = configuration["Authentication:Twitter:ConsumerSecret"];
            string accessToken = oAuthResponse.Oauth_token;
            string tokenSecret = oAuthResponse.Oauth_token_secret;

            var oauth = new OAuthRequest
            {
                Method = "GET",
                Type = OAuthRequestType.ProtectedResource,
                SignatureMethod = OAuthSignatureMethod.HmacSha1,
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                Token = accessToken,
                TokenSecret = tokenSecret,
                RequestUrl = ExternalAPIUrls.TwitterUserInfo,
                Version = "1.0"
            };


            var request = new HttpRequestMessage(HttpMethod.Get, oauth.RequestUrl);
            request.Headers.Add("Authorization", oauth.GetAuthorizationHeader());

            var cookieContainer = new CookieContainer();
            foreach (var item in oAuthResponse.Cookies)
            {
                cookieContainer.Add(new Uri("http://twitter.com"), new Cookie(item.Key, item.Value));
            }

            var handler = new HttpClientHandler();
            handler.CookieContainer = cookieContainer;

            using (HttpClient httpClient = new HttpClient(handler))
            {

                HttpResponseMessage response = await httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {

                    string responseContent = await response.Content.ReadAsStringAsync();
                    twitterUserInfo = JsonSerializer.Deserialize<TwitterUserInfo>(responseContent);
                }
                else
                {

                    await Task.FromException(new BusinessException("Erro Code: " + response.StatusCode));
                }

            }

            return twitterUserInfo;


        }

        public async Task<string> GithubSignIn(string code)
        {
            string client_id = configuration["Authentication:Github:client_id"];
            string client_secret = configuration["Authentication:Github:client_secret"];


            string accessToken = "";
            var postData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", client_id),
                new KeyValuePair<string, string>("client_secret", client_secret),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", "http://localhost:4200/auth/github/callback"),
             });

            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage responseMessage = await httpClient.PostAsync(ExternalAPIUrls.GithubAccessToken, postData);


                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = await responseMessage.Content.ReadAsStringAsync();

                    string accessTokenPrefix = "access_token=";
                    int index = result.IndexOf(accessTokenPrefix);

                    int accessTokenStartIndex = index + accessTokenPrefix.Length;
                    int accessTokenEndIndex = result.IndexOf('&', accessTokenStartIndex);

                    accessToken = result.Substring(accessTokenStartIndex, accessTokenEndIndex - accessTokenStartIndex);
                }
                else
                {

                    await Task.FromException(new BusinessException("Erro Code: " + responseMessage.StatusCode));
                }
            }

            return accessToken;
        }

        public async Task<GithubUserInfo> GithubUserInfo(string bearerToken)
        {
            GithubUserInfo userInfo = new();
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(ExternalAPIUrls.GithubUserInfo);


                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var result = await httpResponseMessage.Content.ReadAsStringAsync();
                    userInfo = JsonSerializer.Deserialize<GithubUserInfo>(result);
                }
                else
                {

                    await Task.FromException(new BusinessException("Erro Code: " + httpResponseMessage.StatusCode));
                }
            }

            return userInfo;
        }
    }

}
