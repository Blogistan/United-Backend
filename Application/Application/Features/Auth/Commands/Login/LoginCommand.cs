using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Application.Dtos;
using Core.Application.Pipelines.Logging;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommand : IRequest<LoginResponse>, ILoggableRequest
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; } = string.Empty;

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
        {
            private readonly IUserRepository userRepository;
            private readonly AuthBussinessRules authBussinessRules;
            private readonly IAuthService authService;

            public LoginCommandHandler(IAuthService authService, AuthBussinessRules authBussinessRules, IUserRepository userRepository)
            {
                this.authService = authService;
                this.authBussinessRules = authBussinessRules;
                this.userRepository = userRepository;
            }

            public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User siteUser = await userRepository.GetAsync(x => x.Email == request.UserForLoginDto.Email);


                await authBussinessRules.UserShouldBeExist(siteUser);

                await authBussinessRules.IsUserActive(siteUser.Id);

                await authBussinessRules.IsUserTimeOut(siteUser.Id);
               
                

                await authBussinessRules.UserPasswordShoudBeMatch(siteUser, request.UserForLoginDto.Password);



                LoginResponse loginResponse = new();

                if (siteUser!.AuthenticatorType is not AuthenticatorType.None)
                {
                    if (request.UserForLoginDto.AuthenticatorCode == null)
                    {
                        await authService.SendAuthenticatorCode(siteUser);
                        loginResponse.RequiredAuthenticatorType = siteUser.AuthenticatorType;

                        return loginResponse;

                    }
                    {
                        await authService.VerifyAuthenticatorCode(siteUser, request.UserForLoginDto.AuthenticatorCode);
                    }
                }

                AccessToken accessToken = await authService.CreateAccessToken(siteUser);

                await authService.DeleteOldActiveRefreshTokens(siteUser);

                RefreshToken refreshToken = authService.CreateRefreshToken(siteUser, request.IpAddress);

                await authService.AddRefreshToken(refreshToken);

                loginResponse.RefreshToken = refreshToken;
                loginResponse.AccessToken = accessToken;
                return loginResponse;

            }
        }

    }
}
