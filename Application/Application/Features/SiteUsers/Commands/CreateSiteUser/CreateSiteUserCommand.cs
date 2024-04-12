using Application.Features.SiteUsers.Rules;
using Application.Notifications.RegisterNotification;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;

namespace Application.Features.SiteUsers.Commands.CreateSiteUser
{
    public class CreateSiteUserCommand : IRequest<CreateSiteUserResponse>, ISecuredRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        string[] ISecuredRequest.Roles => new string[] { "Admin"};

        public CreateSiteUserCommand()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }
        public CreateSiteUserCommand(string firstname, string lastName, string email, string password)
        {
            FirstName = firstname;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public class CreateSiteUserCommandHandler : IRequestHandler<CreateSiteUserCommand, CreateSiteUserResponse>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly IMapper mapper;
            private readonly UserBusinessRules userBusinessRules;
            private readonly IMediator mediator;
            public CreateSiteUserCommandHandler(ISiteUserRepository siteUserRepository, IMapper mapper, UserBusinessRules userBusinessRules, IMediator mediator)
            {
                this.siteUserRepository = siteUserRepository;
                this.mapper = mapper;
                this.userBusinessRules = userBusinessRules;
                this.mediator = mediator;
            }
            public async Task<CreateSiteUserResponse> Handle(CreateSiteUserCommand request, CancellationToken cancellationToken)
            {
                await userBusinessRules.UserEmailShouldNotExistsWhenInsert(request.Email);
                SiteUser user = mapper.Map<SiteUser>(request);

                HashingHelper.CreatePasswordHash(
                    request.Password,
                    passwordHash: out byte[] passwordHash,
                    passwordSalt: out byte[] passwordSalt
                );
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                SiteUser createdUser = await siteUserRepository.AddAsync(user);

                CreateSiteUserResponse response = mapper.Map<CreateSiteUserResponse>(createdUser);

                await mediator.Publish(new RegisteredNotification() { SiteUser = createdUser });

                return response;
            }
        }
    }
}
