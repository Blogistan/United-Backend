using Application.Features.Auth.Commands.EnableEmailAuthenticator;
using Application.Features.Auth.Commands.EnableOtpAuthenticatorCommand;
using Application.Features.Auth.Commands.ExternalLoginUrl.Github;
using Application.Features.Auth.Commands.ExternalLoginUrl.Twitter;
using Application.Features.Auth.Commands.FacebookSignIn;
using Application.Features.Auth.Commands.ForgetPassword;
using Application.Features.Auth.Commands.GithubSignIn;
using Application.Features.Auth.Commands.GoogleSignIn;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.PasswordReset;
using Application.Features.Auth.Commands.Refresh;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.Revoke;
using Application.Features.Auth.Commands.TwitterSignIn;
using Application.Features.Auth.Commands.VerifyEmailAuthenticatorCommand;
using Application.Features.Auth.Commands.VerifyOtpAuthenticatorCommand;
using Application.Services.Auth;
using Core.Application.Dtos;
using Infrastructure.Dtos.Facebook;
using Infrastructure.Dtos.Twitter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using UnitedAPI.ValueObjects;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private WebApiConfigurations webApiConfigurations;
        private IAuthService authService;
        public AuthController(IConfiguration configuration, HttpClient httpClient, IAuthService authService)
        {
            this.webApiConfigurations = configuration.GetSection("WebApiConfigurations").Get<WebApiConfigurations>() ?? throw new ArgumentNullException(nameof(WebApiConfigurations));
            this.authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            LoginResponse loginResponse = await Mediator.Send(new LoginCommand
            {
                UserForLoginDto = userForLoginDto,
                IpAddress = GetIpAddress()
            });
            if (loginResponse.RefreshToken is not null) SetRefreshTokenToCookie(loginResponse.RefreshToken);
            return Ok(loginResponse.ToHttpResponse());
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            RegisteredResponse registeredResponse = await Mediator.Send(new RegisterCommand
            {
                UserForRegisterDto = userForRegisterDto,
                IpAddress = GetIpAddress()
            });
            SetRefreshTokenToCookie(registeredResponse.RefreshToken);
            return Ok(registeredResponse);
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            RefreshedResponse refreshedResponse = await Mediator.Send(new RefreshCommand
            {
                RefreshToken = GetRefreshTokenFromCookie(),
                IpAddress = GetIpAddress()
            });
            if (refreshedResponse is not null) SetRefreshTokenToCookie(refreshedResponse.RefreshToken);
            return Ok(refreshedResponse.AccessToken);
        }
        [HttpPut("Revoke")]
        public async Task<IActionResult> RevokeToken([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] string? refreshToken)
        {
            RevokedResponse revokedResponse = await Mediator.Send(new RevokeCommand
            {
                Token = refreshToken ?? GetRefreshTokenFromCookie(),
                IpAddress = GetIpAddress()
            });

            return Ok(revokedResponse);
        }
        [HttpGet("EnableEmailAuthenticator")]
        public async Task<IActionResult> EnableEmailAuthenticator()
        {
            EnableEmailAuthenticatorCommand command = new()
            {
                UserID = GetUserIdFromToken(),
                VerifyToEmail = webApiConfigurations.AuthVerifyEmailUrl
            };

            await Mediator.Send(command);
            return Ok();
        }


        [HttpPut("VerifyEmailAuthenticator")]
        public async Task<IActionResult> VerifyEmailAuthenticator([FromBody] VerifyEmailAuthenticatorCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("EnableOtpAuthenticator")]
        public async Task<IActionResult> EnableOtpAuthenticator()
        {
            EnableOtpAuthenticatorCommand command = new()
            {
                SecretKeyIssuer = webApiConfigurations.SecretKeyIssuer,
                SecretKeyLabel = webApiConfigurations.SecretKeyLabel,
                UserID = GetUserIdFromToken()
            };

            EnabledOtpAuthenticatorResponse response = await Mediator.Send(command);


            return Ok(response);
        }
        [HttpPut("VerifyOtpAuthenticator")]
        public async Task<IActionResult> VerifyOtpAuthenticator([FromBody] string otpCode)
        {
            VerifyOtpAuthenticatorCommand command = new()
            {

                OtpCode = otpCode,
                UserId = GetUserIdFromToken()

            };

            await Mediator.Send(command);

            return Ok();
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgotPasswordCommandRequest forgotPasswordCommandRequest)
        {
            ForgetPasswordCommand forgetPasswordCommand = new()
            {
                Email = forgotPasswordCommandRequest.Email,
                PasswordResetUrl = webApiConfigurations.PasswordResetUrl
            };
            var result = await Mediator.Send(forgetPasswordCommand);
            return Ok(result);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetCommand passwordResetCommand)
        {
            await Mediator.Send(passwordResetCommand);
            return Ok();
        }

        [HttpPost("GoogleSignIn")]
        public async Task<IActionResult> GoogleSignIn([FromBody] GoogleSignInCommandRequest googleSignInCommandRequest)
        {
            GoogleSignInCommand googleSignInCommand = new GoogleSignInCommand()
            {
                IdToken = googleSignInCommandRequest.IdToken,
                IpAdress = GetIpAddress()
            };
            LoginResponse response = await Mediator.Send(googleSignInCommand);
            SetRefreshTokenToCookie(response.RefreshToken);
            return Ok(response);
        }
        [HttpPost("FacebookSignIn")]
        public async Task<IActionResult> FacbookSignIn([FromBody] FacebookSignInCommandRequest facebookSignInCommandRequest)
        {
            FacebookSignInCommand facebookSignInCommand = new()
            {
                Token = facebookSignInCommandRequest.Token,
                IpAddress = GetIpAddress()
            };

            FacebookLoginResponse facebookLoginResponse = await Mediator.Send(facebookSignInCommand);
            return Ok(facebookLoginResponse);
        }

        [HttpPost("TwitterSignIn")]
        public async Task<IActionResult> TwitterSignIn([FromBody] OAuthCredentials oAuthCredentials)
        {

            var result = await authService.TwitterSignIn(oAuthCredentials);

            LoginResponse loginResponse = await Mediator.Send(new TwitterSignInCommand { AccessToken = result.Oauth_token, TokenSecret = result.Oauth_token_secret, IpAddress = GetIpAddress(), Cookies = result.Cookies });

            return Ok(loginResponse);
        }
        [HttpGet("GetTwitterLoginLink")]
        public async Task<IActionResult> GetTwitterLoginLink()
        {
            GetTwitterLoginLinkCommandResponse response = await Mediator.Send(new GetTwitterLoginLinkCommand());
            return Ok(response);
        }
        [HttpPost("GithubSignIn")]
        public async Task<IActionResult> GithubSignIn([FromBody] GithubSignInCommandRequest githubSignInCommandRequest)
        {
            var result = await authService.GithubSignIn(githubSignInCommandRequest.code);

            LoginResponse loginResponse = await Mediator.Send(new GithubSignInCommand { Token = result, IpAddress = GetIpAddress() });
            return Ok(loginResponse);
        }
        [HttpGet("GetGithubSignUrl")]
        public async Task<IActionResult> GetGithubSignUrl()
        {
            GetGithubLoginLinkCommandResponse response = await Mediator.Send(new GetGithubLoginLinkCommand());
            return Ok(response);
        }
    }
}
