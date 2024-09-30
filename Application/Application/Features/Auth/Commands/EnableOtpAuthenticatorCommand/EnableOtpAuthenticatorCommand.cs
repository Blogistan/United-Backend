using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.EnableOtpAuthenticatorCommand
{
    public class EnableOtpAuthenticatorCommand : IRequest<EnabledOtpAuthenticatorResponse>, ISecuredRequest
    {
        public int UserID { get; set; }
        public string SecretKeyLabel { get; set; }
        public string SecretKeyIssuer { get; set; }
        public string[] Roles => Array.Empty<string>();

        public class EnableOtpAuthenticatorCommandHandler : IRequestHandler<EnableOtpAuthenticatorCommand, EnabledOtpAuthenticatorResponse>
        {
            private AuthBussinessRules AuthBussinessRules;
            private IOtpAuthenticatorRepository otpAuthenticatorRepository;
            private IUserRepository userRepository;
            private IAuthService authService;
            public EnableOtpAuthenticatorCommandHandler(AuthBussinessRules authBussinessRules, IOtpAuthenticatorRepository otpAuthenticatorRepository, IUserRepository userRepository, IAuthService authService)
            {
                this.authService = authService;
                this.AuthBussinessRules = authBussinessRules;
                this.otpAuthenticatorRepository = otpAuthenticatorRepository;
                this.userRepository = userRepository;
            }


            public async Task<EnabledOtpAuthenticatorResponse> Handle(EnableOtpAuthenticatorCommand request, CancellationToken cancellationToken)
            {
                User? user = await userRepository.GetAsync(x => x.Id == request.UserID && x.IsActive==true);

                await AuthBussinessRules.UserShouldBeExist(user);
                await AuthBussinessRules.UserShouldNotBeHasAuthenticator(user);

                await otpAuthenticatorRepository.DeleteAllNonVerifiedAsync(user);


                OtpAuthenticator token = await authService.CreateOtpAuthenticator(user);

                await otpAuthenticatorRepository.AddAsync(token);

                string base32SecretKey = await authService.ConvertOtpKeyToString(token.SecretKey);

                EnabledOtpAuthenticatorResponse response = new()
                {
                    SecretKey = base32SecretKey,
                    SecketKeyUrl = $"otpauth://totp/{request.SecretKeyLabel}?secret={base32SecretKey}&issuer={request.SecretKeyIssuer}"
                };
                return response;

            }
        }
    }
}
