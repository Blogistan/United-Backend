using Application.Features.SiteUsers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;

namespace Application.Features.SiteUsers.Commands.UpdateSiteUser
{
    public class UpdateSiteUserCommand : IRequest<UpdateSiteUserCommandResponse>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public UpdateSiteUserCommand()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }
        public UpdateSiteUserCommand(int id, string firstname, string lastName, string email, string password)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        public class UpdateSiteUserCommandHandler : IRequestHandler<UpdateSiteUserCommand, UpdateSiteUserCommandResponse>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly IMapper mapper;
            private readonly UserBusinessRules userBusinessRules;

            public UpdateSiteUserCommandHandler(ISiteUserRepository siteUserRepository, IMapper mapper, UserBusinessRules userBusinessRules)
            {
                this.siteUserRepository = siteUserRepository;
                this.mapper = mapper;
                this.userBusinessRules = userBusinessRules;
            }

            public async Task<UpdateSiteUserCommandResponse> Handle(UpdateSiteUserCommand request, CancellationToken cancellationToken)
            {
                SiteUser? siteUser = await siteUserRepository.GetAsync(x => x.Id.Equals(request.Id), cancellationToken: cancellationToken,enableTracking:false);

                await userBusinessRules.UserShouldBeExistsWhenSelected(siteUser);
                await userBusinessRules.UserEmailShouldNotExistsWhenUpdate(request.Id, request.Email);

                siteUser = mapper.Map<SiteUser>(request);

                HashingHelper.CreatePasswordHash(request.Password, passwordHash: out byte[] passwordHash, passwordSalt: out byte[] passwordSalt);

                siteUser!.PasswordHash = passwordHash;
                siteUser!.PasswordSalt = passwordSalt;

                await siteUserRepository.UpdateAsync(siteUser);

                var response = mapper.Map<UpdateSiteUserCommandResponse>(siteUser);

                return response;
            }
        }
    }
}
