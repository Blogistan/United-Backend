using Application.Features.Auth.Commands.EnableEmailAuthenticator;
using Application.Features.Auth.Commands.EnableOtpAuthenticatorCommand;
using Application.Features.Auth.Commands.FacebookSignIn;
using Application.Features.Auth.Commands.ForgetPassword;
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
            if (loginResponse is not null) SetRefreshTokenToCookie(loginResponse.RefreshToken);
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


        [HttpGet("VerifyEmailAuthenticator")]// Verify Email URL api'a yönlendirdiği için GET kullandık. Bir frontend yardımıyla yapılırsa PUT olabilir.
        public async Task<IActionResult> VerifyEmailAuthenticator([FromQuery] string ActivationKey)
        {
            VerifyEmailAuthenticatorCommand command = new()
            {
                ActivationKey = ActivationKey
            };
            await Mediator.Send(command);
            return Ok("Verification success");
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
        public async Task<IActionResult> ForgetPassword(string email)
        {
            ForgetPasswordCommand forgetPasswordCommand = new()
            {
                Email = email,
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
        public async Task<IActionResult> GoogleSignIn([FromBody] string token)
        {
            GoogleSignInCommand googleSignInCommand = new GoogleSignInCommand()
            {
                IdToken = token,
                IpAdress = GetIpAddress()
            };
            LoginResponse response = await Mediator.Send(googleSignInCommand);
            SetRefreshTokenToCookie(response.RefreshToken);
            return Ok(response);
        }
        [HttpPost("FacebookSignIn")]
        public async Task<IActionResult> FacbookSignIn([FromBody] string Token)
        {
            FacebookSignInCommand facebookSignInCommand = new()
            {
                Token = Token,
                IpAddress = GetIpAddress()
            };

            FacebookLoginResponse facebookLoginResponse = await Mediator.Send(facebookSignInCommand);
            return Ok(facebookLoginResponse);
        }

        [HttpGet("TwitterSignIn")]
        public async Task<IActionResult> TwitterSignIn(string oauth_token, string oauth_verifier)
        {

            var result = await authService.TwitterSignIn(new Infrastructure.Dtos.Twitter.OAuthCredentials()
            {
                Oauth_token = oauth_token,
                Oauth_verifier = oauth_verifier
            });

            LoginResponse loginResponse = await Mediator.Send(new TwitterSignInCommand { AccessToken = result.Oauth_token, TokenSecret = result.Oauth_token_secret, IpAddress = GetIpAddress(), Cookies = result.Cookies });

            //var userInfo = await authService.GetTwitterUserInfo(result);
            return Ok(loginResponse);
        }     
    }
}
