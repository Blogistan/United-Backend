using Application.Features.Auth.Commands.EnableEmailAuthenticator;
using Application.Features.Auth.Commands.EnableOtpAuthenticatorCommand;
using Application.Features.Auth.Commands.ForgetPassword;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.PasswordReset;
using Application.Features.Auth.Commands.Refresh;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Commands.Revoke;
using Application.Features.Auth.Commands.VerifyEmailAuthenticatorCommand;
using Application.Features.Auth.Commands.VerifyOtpAuthenticatorCommand;
using Core.Application.Dtos;
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
        public AuthController(IConfiguration configuration)
        {
            this.webApiConfigurations = configuration.GetSection("WebApiConfigurations").Get<WebApiConfigurations>() ?? throw new ArgumentNullException(nameof(WebApiConfigurations));
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



    }
}
