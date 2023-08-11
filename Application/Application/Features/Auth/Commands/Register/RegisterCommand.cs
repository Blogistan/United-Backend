using Application.Features.Auth.Rules;
using Application.Notifications.RegisterNotification;
using Application.Services.Auth;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<RegisteredResponse>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; } = string.Empty;


        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly AuthBussinessRules authBussinessRules;
            private readonly IAuthService authService;
            private readonly IMapper mapper;
            private readonly IMediator mediator;
            public RegisterCommandHandler(ISiteUserRepository siteUserRepository, AuthBussinessRules authBussinessRules, IAuthService authService, IMapper mapper, IMediator mediator)
            {
                this.mapper = mapper;
                this.siteUserRepository = siteUserRepository;
                this.authBussinessRules = authBussinessRules;
                this.authService = authService;
                this.mediator = mediator;
            }

            public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await authBussinessRules.UserEmailCannotBeDuplicatedWhenInserted(request.UserForRegisterDto.Email);

                byte[] hash, salt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out hash, out salt);

                SiteUser siteUser = mapper.Map<SiteUser>(request.UserForRegisterDto);

                siteUser.PasswordHash = hash;
                siteUser.PasswordSalt = salt;
                siteUser.Status = true;

                await siteUserRepository.AddAsync(siteUser);

                AccessToken accessToken = await authService.CreateAccessToken(siteUser);
                RefreshToken refreshToken = await authService.CreateRefreshToken(siteUser, request.IpAddress);
                await authService.AddRefreshToken(refreshToken);
                refreshToken.User = null;
                RegisteredResponse registeredResponse = new()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };

                await mediator.Publish(new RegisteredNotification() { SiteUser = siteUser });

                return registeredResponse;



            }
        }
    }
}
