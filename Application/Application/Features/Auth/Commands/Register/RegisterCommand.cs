using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Notifications.RegisterNotification;
using Application.Services.Auth;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Dtos;
using Core.Application.Pipelines.Logging;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<RegisteredResponse>, ILoggableRequest
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; } = string.Empty;


        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly IUserRepository userRepository;
            private readonly AuthBussinessRules authBussinessRules;
            private readonly IAuthService authService;
            private readonly IMapper mapper;
            private readonly IMediator mediator;
            private readonly IUserOperationClaimRepository userOperationClaimRepository;
            public RegisterCommandHandler(ISiteUserRepository siteUserRepository, AuthBussinessRules authBussinessRules, IAuthService authService, IMapper mapper, IMediator mediator, IUserOperationClaimRepository userOperationClaimRepository, IUserRepository userRepository)
            {
                this.mapper = mapper;
                this.siteUserRepository = siteUserRepository;
                this.authBussinessRules = authBussinessRules;
                this.authService = authService;
                this.mediator = mediator;
                this.userOperationClaimRepository = userOperationClaimRepository;
                this.userRepository = userRepository;
            }

            public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await authBussinessRules.UserEmailCannotBeDuplicatedWhenInserted(request.UserForRegisterDto.Email);

                byte[] hash, salt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out hash, out salt);

                SiteUser siteUser = mapper.Map<SiteUser>(request.UserForRegisterDto);               
                siteUser.User.PasswordHash = hash;
                siteUser.User.PasswordSalt = salt;
                siteUser.User.IsActive = true;
                siteUser.IsVerified = false;

                var createdUser= await siteUserRepository.AddAsync(siteUser);
                await userOperationClaimRepository.AddAsync(new UserOperationClaim { UserId = createdUser.UserId, OperationClaimId = 3 });
                AccessToken accessToken = await authService.CreateAccessToken(createdUser.User);
                RefreshToken refreshToken = authService.CreateRefreshToken(createdUser.User, request.IpAddress);
                await authService.AddRefreshToken(refreshToken);
                refreshToken.User = createdUser.User;


                RegisteredResponse registeredResponse = new()
                {
                    AccessToken = mapper.Map<AccessTokenDto>(accessToken),
                    RefreshToken = refreshToken
                };

                await mediator.Publish(new RegisteredNotification() { SiteUser = createdUser.User });

                return registeredResponse;



            }
        }
    }
}
