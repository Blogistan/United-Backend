using Application.Features.Auth.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Mailing;
using Core.Persistence.Paging;
using Core.Security.EmailAuthenticator;
using Core.Security.Entities;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator;
using Domain.Entities;
using Google.Apis.Auth;
using Infrastructure.Constants;
using Infrastructure.Dtos;
using Infrastructure.Dtos.Facebook;
using Infrastructure.Dtos.Google;
using Infrastructure.Dtos.Twitter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
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

        public AuthService(ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository, ISiteUserRepository siteUserRepository, IEmailAuthenticatorRepository emailAuthenticatorRepository, IUserOperationClaimRepository userOperationClaimRepository, IMailService mailService,
            IOtpAuthenticatorHelper otpAuthenticatorHelper,
            IEmailAuthenticatorHelper emailAuthenticatorHelper,
            IOtpAuthenticatorRepository otpAuthenticatorRepository, HttpClient httpClient, IConfiguration configuration)
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

        public async Task<RefreshToken> CreateRefreshToken(User user, string IpAddress)
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

        public async Task<LoginResponseBase> CreateUserExternalAsync(SiteUser user, string email, string name, string surname, string picture, string ipAdress)
        {
            bool result = user != null;

            AccessToken accessToken = new();
            RefreshToken refreshToken = new();

            GoogleLoginResponse loginResponse = new();

            if (user == null)
            {
                SiteUser siteUser = new()
                {
                    FirstName = name,
                    LastName = surname,
                    Email = email,
                    IsVerified = false,
                    ProfileImageUrl = picture,
                };

                var createdUser = await siteUserRepository.AddAsync(siteUser);
                accessToken = await CreateAccessToken(siteUser);
                refreshToken = await CreateRefreshToken(siteUser, ipAdress);

                await AddRefreshToken(refreshToken);

                loginResponse.RefreshToken = refreshToken;
                loginResponse.AccessToken = accessToken;
            }
            else
            {
                accessToken = await CreateAccessToken(user);
                refreshToken = await CreateRefreshToken(user, ipAdress);
                await AddRefreshToken(refreshToken);

                loginResponse.RefreshToken = refreshToken;
                loginResponse.AccessToken = accessToken;
            }

            return loginResponse;
        }

        public async Task<GoogleJsonWebSignature.Payload> GoogleSignIn(string idToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { configuration["Google:client_id"] }
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

            string nonce = GenerateNonce();
            string timestamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();

            var requestParams = new SortedDictionary<string, string>
            {
                { "oauth_consumer_key", consumerKey },
                { "oauth_nonce", nonce },
                { "oauth_signature_method", "HMAC-SHA1" },
                { "oauth_timestamp", timestamp },
                { "oauth_token", accessToken },
                { "oauth_version", "1.0" }
            };


            string signatureBase = "GET&" + Uri.EscapeDataString(ExternalAPIUrls.UserInfo) + "&" + Uri.EscapeDataString(string.Join("&", requestParams.Select(kv => $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}")));
            string signingKey = Uri.EscapeDataString(consumerSecret) + "&" + Uri.EscapeDataString(tokenSecret);
            string signature = ComputeHMACSHA1Signature(signatureBase, signingKey);

            requestParams.Add("oauth_signature", signature);
          
            var cookieContainer = new CookieContainer();
            foreach (var item in oAuthResponse.Cookies)
            {
                cookieContainer.Add(new Uri("http://twitter.com"), new Cookie(item.Key, item.Value));
            }

            var handler = new HttpClientHandler();
            handler.CookieContainer = cookieContainer;

            using (HttpClient httpClient = new HttpClient(handler))
            {
                var header = "OAuth " + string.Join(",", requestParams.Keys.Select(key => $"{key}=\"{Uri.EscapeDataString(requestParams[key])}\""));

                httpClient.DefaultRequestHeaders.Add("Authorization", header);


                var content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.GetAsync(ExternalAPIUrls.UserInfo);

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
        static string ComputeHMACSHA1Signature(string data, string key)
        {
            using (var hmacsha1 = new HMACSHA1(Encoding.ASCII.GetBytes(key)))
            {
                var hashBytes = hmacsha1.ComputeHash(Encoding.ASCII.GetBytes(data));
                return Convert.ToBase64String(hashBytes);
            }
        }
        static string GenerateNonce(int length = 32)
        {
            byte[] randomBytes = new byte[length];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            string base64Nonce = Convert.ToBase64String(randomBytes);
            string cleanNonce = RemoveInvalidChars(base64Nonce);

            string timestamp = GetTimestamp();
            string nonceWithTimestamp = timestamp + cleanNonce;

            return nonceWithTimestamp.Length <= length ? nonceWithTimestamp : nonceWithTimestamp.Substring(0, length);
        }

        static string GetTimestamp()
        {
            return Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString();
        }

        static string RemoveInvalidChars(string input)
        {
            // Remove characters that are not valid in a nonce
            return new string(input
                .Where(c => char.IsLetterOrDigit(c) || c == '+' || c == '/' || c == '=')
                .ToArray());
        }


    }
}
