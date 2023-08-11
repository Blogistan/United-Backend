using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.Revoke
{
    public class RevokeCommand:IRequest<RevokedResponse>
    {
        public string Token { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;

        public class RevokeCommandHandler:IRequestHandler<RevokeCommand,RevokedResponse>
        {
            private readonly IRefreshTokenRepository refreshTokenRepository;
            private AuthBussinessRules AuthBussinessRules;
            private IAuthService authService;
            private IMapper mapper;
            public RevokeCommandHandler(IRefreshTokenRepository refreshTokenRepository, AuthBussinessRules authBussinessRules, IAuthService authService,IMapper mapper)
            {
                this.mapper = mapper;
                this.refreshTokenRepository = refreshTokenRepository;
                this.AuthBussinessRules = authBussinessRules;
                this.authService = authService;
            }

            public async Task<RevokedResponse> Handle(RevokeCommand request, CancellationToken cancellationToken)
            {
                RefreshToken refreshToken = await refreshTokenRepository.GetAsync(x=>x.Token==request.Token);

                await AuthBussinessRules.RefreshTokenShouldBeExist(refreshToken);
                await AuthBussinessRules.RefreshTokenShouldBeActive(refreshToken);

                await authService.RevokeRefreshToken(refreshToken,request.IpAddress,"Refresh token revoked manualy.",null);

                RevokedResponse revokedResponse = mapper.Map<RevokedResponse>(refreshToken);

                return revokedResponse;

            }
        }
    }
}
