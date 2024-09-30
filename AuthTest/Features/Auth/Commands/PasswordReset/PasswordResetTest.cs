using Application.Features.Auth.Commands.PasswordReset;
using Application.Features.Auth.Profiles;
using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using AuthTest.Mocks.Configurations;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using AuthTest.Mocks.Repositories.Auth;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using Core.Security.EmailAuthenticator;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator;
using Core.Security.OtpAuthenticator.OtpNet;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.Extensions.Configuration;
using static Application.Features.Auth.Commands.PasswordReset.PasswordResetCommand;

namespace AuthTest.Features.Auth.Commands.PasswordReset
{
    public class PasswordResetTest : IClassFixture<Startup>
    {
        private readonly PasswordResetCommand passwordResetCommand;
        private readonly PasswordResetCommandHandler passwordResetCommandHandler;
        private readonly PasswordResetCommandValidator validationRules;
        private readonly IConfiguration configuration;
        private readonly IMediator mediator;

        public PasswordResetTest(RefreshTokenFakeData refreshTokenFakeData,
           SiteUserFakeData siteUserFakeData, OperationClaimFakeData operationClaimFakeData, UserOperationClaimFakeData userOperationClaimFakeData, BanFakeData banFakeData, ForgotPasswordFakeData forgotPasswordFakeData, IMediator mediator,UserLoginFakeData userLoginFakeData)
        {

            #region Mock Repositories
            this.configuration = MockConfiguration.GetMockConfiguration();
            IUserOperationClaimRepository userOperationClaimRepository = new MockUserOperationClaimRepository(userOperationClaimFakeData, operationClaimFakeData).GetOperationClaimRepostiory();
            IRefreshTokenRepository refreshTokenRepository = new MockRefreshTokenRepository(refreshTokenFakeData,siteUserFakeData).GetRefreshTokenRepository();
            IEmailAuthenticatorRepository userEmailAuthenticatorRepository =
            MockEmailAuthenticatorRepository.GetEmailAuthenticatorRepositoryMock();
            IOtpAuthenticatorRepository otpAuthenticatorRepository = MockOtpAuthRepository.GetOtpAuthenticatorRepository();
            ISiteUserRepository siteUserRepository = new MockUserRepository(siteUserFakeData, banFakeData).GetSiteUserRepository();
            IForgotPasswordRepository forgotPasswordRepository = new MockForgotPasswordRepository(siteUserFakeData, forgotPasswordFakeData).GetForgotPasswordRepository();
            IUserLoginRepository userLoginRepository = MockUserLoginRepository.GetUserLoginRepository(userLoginFakeData).Object;
            #endregion

            #region Mock Helpers


            TokenOptions tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>() ?? throw new Exception("Token options not found.");
            ITokenHelper tokenHelper = new JwtHelper(configuration);
            IEmailAuthenticatorHelper emailAuthenticatorHelper = new EmailAuthenticatorHelper();
            IMailService mailService = new MailKitMailService(configuration);
            IOtpAuthenticatorHelper otpAuthenticatorHelper = new OtpNetOtpAuthenticatorHelper();
            IMapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()));

            #endregion
            HttpClient httpClient = new HttpClient();
            IAuthService authService = new AuthService(tokenHelper, refreshTokenRepository, siteUserRepository, userEmailAuthenticatorRepository, userOperationClaimRepository, mailService, otpAuthenticatorHelper, emailAuthenticatorHelper, otpAuthenticatorRepository, httpClient, configuration,userLoginRepository);
            //AuthBussinessRules authBussinessRules = new AuthBussinessRules(siteUserRepository);

            this.mediator = mediator;
            this.passwordResetCommand = new PasswordResetCommand();
            this.validationRules = new PasswordResetCommandValidator();
            //this.passwordResetCommandHandler = new PasswordResetCommandHandler(authService, siteUserRepository, authBussinessRules, forgotPasswordRepository, mailService, mediator);

        }
        [Fact]
        public async Task PasswordResetKeyShouldBeExists()
        {
            passwordResetCommand.NewPassword = "123123123";
            passwordResetCommand.NewPasswordConfirm = "123123123";
            passwordResetCommand.ResetKey = "AAABBB221";

           await Assert.ThrowsAsync<BusinessException>(async () =>
            {
               await passwordResetCommandHandler.Handle(passwordResetCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task PasswordResetTokenShouldBeActive()
        {
            passwordResetCommand.NewPassword = "123123123";
            passwordResetCommand.NewPasswordConfirm = "123123123";
            passwordResetCommand.ResetKey = "A2C6C3";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await passwordResetCommandHandler.Handle(passwordResetCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task ResetTokenShouldHaveValidExpirationTime()
        {
            passwordResetCommand.NewPassword = "123123123";
            passwordResetCommand.NewPasswordConfirm = "123123123";
            passwordResetCommand.ResetKey = "A2B1C3";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await passwordResetCommandHandler.Handle(passwordResetCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async  Task NewPasswordMustBeConfirmed()
        {
            passwordResetCommand.NewPassword = "123123123";
            passwordResetCommand.NewPasswordConfirm = "1231231232323";
            passwordResetCommand.ResetKey = "A2B2C3";

            TestValidationResult<PasswordResetCommand> testValidationResult= validationRules.TestValidate<PasswordResetCommand>(passwordResetCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.NewPassword);
        }
            
    }
}
