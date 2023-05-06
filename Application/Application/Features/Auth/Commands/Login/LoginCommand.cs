﻿using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Application.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly AuthBussinessRules authBussinessRules;
            private readonly IAuthService authService;

            public LoginCommandHandler(IAuthService authService, AuthBussinessRules authBussinessRules, ISiteUserRepository siteUserRepository)
            {
                this.authService = authService;
                this.authBussinessRules = authBussinessRules;
                this.siteUserRepository = siteUserRepository;
            }

            public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                SiteUser siteUser = await siteUserRepository.GetAsync(x => x.Email == request.UserForLoginDto.Email);

                await authBussinessRules.UserShouldBeExist(siteUser);

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

                RefreshToken refreshToken = await authService.CreateRefreshToken(siteUser, request.IpAddress);

                await authService.AddRefreshToken(refreshToken);

                loginResponse.RefreshToken = refreshToken;
                loginResponse.AccessToken = accessToken;
                return loginResponse;

            }
        }

    }
}