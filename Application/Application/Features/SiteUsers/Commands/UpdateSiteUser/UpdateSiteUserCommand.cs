using Application.Features.SiteUsers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;

namespace Application.Features.SiteUsers.Commands.UpdateSiteUser
{
    public class UpdateSiteUserCommand : IRequest<UpdateSiteUserCommandResponse>, ISecuredRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Biography { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string ProfileImageUrl { get; set; }
        string[] ISecuredRequest.Roles => new string[] { "Admin" };

        public UpdateSiteUserCommand()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            OldPassword = string.Empty;
        }
        public UpdateSiteUserCommand(int id, string firstname, string lastName, string email, string oldPassword, string newPassword)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastName;
            Email = email;
            OldPassword = oldPassword;
            NewPassword = newPassword;
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
                SiteUser? siteUser = await siteUserRepository.GetAsync(x => x.Id.Equals(request.Id), cancellationToken: cancellationToken, enableTracking: false);

                await userBusinessRules.UserShouldBeExistsWhenSelected(siteUser);
                await userBusinessRules.UserEmailShouldNotExistsWhenUpdate(request.Id, request.Email);

                if (string.IsNullOrEmpty(request.OldPassword) && string.IsNullOrEmpty(request.NewPassword))
                {
                    siteUser!.FirstName = request.FirstName;
                    siteUser!.LastName = request.LastName;
                    siteUser!.Email = request.Email;
                    siteUser!.ProfileImageUrl = request.ProfileImageUrl;
                    siteUser!.Biography = request.Biography;
                }
                else
                {
                    await userBusinessRules.UserPasswordShouldBeMatchBeforeUpdate(siteUser, request.OldPassword);
                    siteUser = mapper.Map<SiteUser>(request);

                    HashingHelper.CreatePasswordHash(request.NewPassword, passwordHash: out byte[] passwordHash, passwordSalt: out byte[] passwordSalt);

                    siteUser!.PasswordHash = passwordHash;
                    siteUser!.PasswordSalt = passwordSalt;
                }

                await siteUserRepository.UpdateAsync(siteUser);

                var response = mapper.Map<UpdateSiteUserCommandResponse>(siteUser);

                return response;
            }
        }
    }
}
