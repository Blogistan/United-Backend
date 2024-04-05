using Application.Features.SiteUsers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;

namespace Application.Features.SiteUsers.Commands
{
    public class CreateSiteUserCommand : IRequest<CreatedSiteUserResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public CreateSiteUserCommand()
        {
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Email = string.Empty;
            this.Password = string.Empty;
        }
        public CreateSiteUserCommand(string firstname, string lastName, string email, string password)
        {
            this.FirstName = firstname;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
        }

        public class CreateSiteUserCommandHandler : IRequestHandler<CreateSiteUserCommand, CreatedSiteUserResponse>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly IMapper mapper;
            private readonly UserBusinessRules userBusinessRules;
            public CreateSiteUserCommandHandler(ISiteUserRepository siteUserRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                this.siteUserRepository = siteUserRepository;
                this.mapper = mapper;
                this.userBusinessRules = userBusinessRules;
            }
            public async Task<CreatedSiteUserResponse> Handle(CreateSiteUserCommand request, CancellationToken cancellationToken)
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

                CreatedSiteUserResponse response = mapper.Map<CreatedSiteUserResponse>(createdUser);
                return response;
            }
        }
    }
}
