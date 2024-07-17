using Application.Features.Auth.Commands.VerifyEmailAuthenticatorCommand;
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
using Microsoft.Extensions.Configuration;
using static Application.Features.Auth.Commands.VerifyEmailAuthenticatorCommand.VerifyEmailAuthenticatorCommand;

namespace AuthTest.Features.Auth.Commands.VerifyEmailAuthenticator
{
    public class VerifiyEmailAuthenticator : IClassFixture<Startup>
    {
        private readonly VerifyEmailAuthenticatorCommand verifyEmailAuthenticatorCommand;
        private readonly VerifyEmailAuthenticatorCommandHandler verifyEmailAuthenticatorCommandHandler;
        private readonly VerifyEmailAuthenticatorCommandValidator validationRules;
        private readonly IConfiguration configuration;
        public VerifiyEmailAuthenticator(RefreshTokenFakeData refreshTokenFakeData,
            SiteUserFakeData siteUserFakeData, OperationClaimFakeData operationClaimFakeData, UserOperationClaimFakeData userOperationClaimFakeData, BanFakeData banFakeData, UserLoginFakeData userLoginFakeData)
        {

            #region Mock Repositories
            this.configuration = MockConfiguration.GetMockConfiguration();
            IUserOperationClaimRepository userOperationClaimRepository = new MockUserOperationClaimRepository(userOperationClaimFakeData, operationClaimFakeData).GetOperationClaimRepostiory();
            IRefreshTokenRepository refreshTokenRepository = new MockRefreshTokenRepository(refreshTokenFakeData, siteUserFakeData).GetRefreshTokenRepository();
            IEmailAuthenticatorRepository userEmailAuthenticatorRepository =
            MockEmailAuthenticatorRepository.GetEmailAuthenticatorRepositoryMock();
            IOtpAuthenticatorRepository otpAuthenticatorRepository = MockOtpAuthRepository.GetOtpAuthenticatorRepository();
            ISiteUserRepository siteUserRepository = new MockUserRepository(siteUserFakeData, banFakeData).GetSiteUserRepository();
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
            IAuthService authService = new AuthService(tokenHelper, refreshTokenRepository, siteUserRepository, userEmailAuthenticatorRepository, userOperationClaimRepository, mailService, otpAuthenticatorHelper, emailAuthenticatorHelper, otpAuthenticatorRepository, httpClient, configuration, userLoginRepository);
            AuthBussinessRules authBussinessRules = new AuthBussinessRules(siteUserRepository);


            this.verifyEmailAuthenticatorCommand = new VerifyEmailAuthenticatorCommand();
            this.validationRules = new VerifyEmailAuthenticatorCommandValidator();
            this.verifyEmailAuthenticatorCommandHandler = new VerifyEmailAuthenticatorCommandHandler(userEmailAuthenticatorRepository, authBussinessRules);
        }
        [Fact]
        public async Task ThrowExcepitonIfActivationKeyEmpty()
        {
            verifyEmailAuthenticatorCommand.ActivationKey = string.Empty;

            TestValidationResult<VerifyEmailAuthenticatorCommand> testValidationResult = await validationRules.TestValidateAsync(verifyEmailAuthenticatorCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.ActivationKey);

        }
        [Fact]
        public async Task UserEmailAuthenticatorShouldBeExists()
        {
            verifyEmailAuthenticatorCommand.ActivationKey = "T1112";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await verifyEmailAuthenticatorCommandHandler.Handle(verifyEmailAuthenticatorCommand, CancellationToken.None);
            });

        }
    }
}
