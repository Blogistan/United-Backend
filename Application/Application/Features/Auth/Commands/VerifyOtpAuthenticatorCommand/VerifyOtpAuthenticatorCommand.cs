using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using Core.Security.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Auth.Commands.VerifyOtpAuthenticatorCommand
{
    public class VerifyOtpAuthenticatorCommand : IRequest, ISecuredRequest
    {
        public int UserId { get; set; }
        public string OtpCode { get; set; }

        public string[] Roles => Array.Empty<string>();

    }

    public class VerifyOtpAuthenticatorCommandHandler : IRequestHandler<VerifyOtpAuthenticatorCommand>
    {
        private IOtpAuthenticatorRepository otpAuthenticatorRepository;
        private AuthBussinessRules authBussinessRules;
        private IAuthService authService;
        public VerifyOtpAuthenticatorCommandHandler(IOtpAuthenticatorRepository otpAuthenticatorRepository, AuthBussinessRules authBussinessRules, IAuthService authService)
        {
            this.otpAuthenticatorRepository = otpAuthenticatorRepository;
            this.authService = authService;
            this.authBussinessRules = authBussinessRules;
        }

        public async Task Handle(VerifyOtpAuthenticatorCommand request, CancellationToken cancellationToken)
        {
            OtpAuthenticator otpAuthenticator = await otpAuthenticatorRepository.GetAsync(predicate: x => x.UserId == request.UserId, include: x => x.Include(x => x.User));

            await authBussinessRules.UserOtpAuthenticatorShouldBeExists(otpAuthenticator);


            otpAuthenticator!.User.AuthenticatorType = AuthenticatorType.Otp;

            await authService.VerifyAuthenticatorCode(otpAuthenticator.User, request.OtpCode);
            otpAuthenticator.IsVerified = true;

            await otpAuthenticatorRepository.UpdateAsync(otpAuthenticator);

            
        }
    }



}
