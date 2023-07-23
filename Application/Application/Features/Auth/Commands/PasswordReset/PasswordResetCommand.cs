using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Security.Hashing;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.PasswordReset
{
    public class PasswordResetCommand : IRequest<Unit>
    {
        public int UserId { get; set; }

        public string NewPassword { get; set; }
        public string PasswordConfirm { get; set; }

        public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommand, Unit>
        {
            private readonly IAuthService authService;
            private readonly ISiteUserRepository siteUserRepository;
            private readonly AuthBussinessRules authBussinessRules;

            //To Do:Password reset mail 
            public PasswordResetCommandHandler(IAuthService authService, ISiteUserRepository siteUserRepository, AuthBussinessRules authBussinessRules)
            {
                this.authService = authService;
                this.siteUserRepository = siteUserRepository;
                this.authBussinessRules = authBussinessRules;
            }

            public async Task<Unit> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
            {
                var user = await siteUserRepository.GetAsync(x => x.Id == request.UserId);
                await authBussinessRules.UserShouldBeExist(user);

                byte[] newHash, salt;

                salt = user.PasswordSalt;

                HashingHelper.CreatePasswordHash(request.NewPassword, out newHash, out salt);

                user.PasswordSalt = salt;
                user.PasswordHash = newHash;


                await siteUserRepository.UpdateAsync(user);

                return Unit.Value;


            }
        }
    }
}
