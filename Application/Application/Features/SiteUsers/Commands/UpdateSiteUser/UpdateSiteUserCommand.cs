using Application.Features.SiteUsers.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                SiteUser? siteUser = await siteUserRepository.GetAsync(x => x.Id.Equals(request.Id), cancellationToken: cancellationToken, enableTracking: false,include:x=>x.Include(x=>x.User));

                await userBusinessRules.UserShouldBeExistsWhenSelected(siteUser.User);
                await userBusinessRules.UserEmailShouldNotExistsWhenUpdate(request.Id, request.Email);

                if (string.IsNullOrEmpty(request.OldPassword) && string.IsNullOrEmpty(request.NewPassword))
                {
                    siteUser!.User.FirstName = request.FirstName;
                    siteUser!.User.LastName = request.LastName;
                    siteUser!.User.Email = request.Email;
                    siteUser!.ProfileImageUrl = request.ProfileImageUrl;
                    siteUser!.Biography = request.Biography;
                }
                else
                {
                    await userBusinessRules.UserPasswordShouldBeMatchBeforeUpdate(siteUser.User, request.OldPassword);
                    siteUser = mapper.Map<SiteUser>(request);

                    HashingHelper.CreatePasswordHash(request.NewPassword, passwordHash: out byte[] passwordHash, passwordSalt: out byte[] passwordSalt);

                    siteUser!.User.PasswordHash = passwordHash;
                    siteUser!.User.PasswordSalt = passwordSalt;
                }

                await siteUserRepository.UpdateAsync(siteUser);

                var response = mapper.Map<UpdateSiteUserCommandResponse>(siteUser);

                return response;
            }
        }
    }
}
