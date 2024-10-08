﻿using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands.Refresh
{
    public class RefreshCommand : IRequest<RefreshedResponse>
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;

        public class RefreshCommandHandler : IRequestHandler<RefreshCommand, RefreshedResponse>
        {
            private readonly IAuthService authService;
            private IRefreshTokenRepository refreshTokenRepository;
            private ISiteUserRepository siteUserRepository;
            private AuthBussinessRules AuthBussinessRules;

            public RefreshCommandHandler(ISiteUserRepository siteUserRepository, IAuthService authService, IRefreshTokenRepository refreshTokenRepository, AuthBussinessRules authBussinessRules)
            {
                this.siteUserRepository = siteUserRepository;
                this.authService = authService;
                this.refreshTokenRepository = refreshTokenRepository;
                AuthBussinessRules = authBussinessRules;
            }

            public async Task<RefreshedResponse> Handle(RefreshCommand request, CancellationToken cancellationToken)
            {
                RefreshToken? refreshToken = await refreshTokenRepository.GetAsync(rt => rt.Token == request.RefreshToken && rt.CreatedByIp==request.IpAddress);
                SiteUser? siteUser = refreshToken != null ? await siteUserRepository.GetAsync(x => x.Id == refreshToken.UserId) : new SiteUser();

                await AuthBussinessRules.RefreshTokenShouldBeExist(refreshToken);

                if (refreshToken!.Revoked != null)
                    await authService.RevokeDescendantRefreshTokens(refreshToken, request.IpAddress, $"Invalid token tried to use: {refreshToken.Token}");
                await AuthBussinessRules.RefreshTokenShouldBeActive(refreshToken);

                AccessToken createdAcccessToken = await authService.CreateAccessToken(siteUser.User);
                RefreshToken createdRefreshToken = authService.CreateRefreshToken(siteUser.User, request.IpAddress);

                RefreshedResponse refreshedResponse = new()
                {
                    AccessToken = createdAcccessToken,
                    RefreshToken = createdRefreshToken
                };

                return refreshedResponse;
            }
        }
    }
}
