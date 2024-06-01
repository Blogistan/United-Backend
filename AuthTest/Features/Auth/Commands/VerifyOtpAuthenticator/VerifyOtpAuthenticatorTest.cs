using Application.Features.Auth.Commands.VerifyOtpAuthenticatorCommand;
using Application.Features.Auth.Profiles;
using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using AuthTest.Mocks.Configurations;
using AuthTest.Mocks.FakeDatas;
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

namespace AuthTest.Features.Auth.Commands.VerifyOtpAuthenticator
{
    public class VerifyOtpAuthenticatorTest : IClassFixture<Startup>
    {
        private readonly VerifyOtpAuthenticatorCommand verifyOtpAuthenticatorCommand;
        private readonly VerifyOtpAuthenticatorCommandHandler verifyOtpAuthenticatorCommandHandler;
        private readonly VerifyOtpAuthenticatorCommandValidator validationRules;
        private readonly IConfiguration configuration;
        public VerifyOtpAuthenticatorTest(RefreshTokenFakeData refreshTokenFakeData,
            SiteUserFakeData siteUserFakeData, OperationClaimFakeData operationClaimFakeData, UserOperationClaimFakeData userOperationClaimFakeData, BanFakeData banFakeData, OtpAuthenticatorFakeData otpAuthenticatorFakeData)
        {

            #region Mock Repositories
            this.configuration = MockConfiguration.GetMockConfiguration();
            IUserOperationClaimRepository userOperationClaimRepository = new MockUserOperationClaimRepository(userOperationClaimFakeData, operationClaimFakeData).GetOperationClaimRepostiory();
            IRefreshTokenRepository refreshTokenRepository = new MockRefreshTokenRepository(refreshTokenFakeData, siteUserFakeData).GetRefreshTokenRepository();
            IEmailAuthenticatorRepository userEmailAuthenticatorRepository =
            MockEmailAuthenticatorRepository.GetEmailAuthenticatorRepositoryMock();
            IOtpAuthenticatorRepository otpAuthenticatorRepository = MockOtpAuthRepository.GetOtpAuthenticatorRepository();
            ISiteUserRepository siteUserRepository = new MockUserRepository(siteUserFakeData, banFakeData).GetSiteUserRepository();
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
            IAuthService authService = new AuthService(tokenHelper, refreshTokenRepository, siteUserRepository, userEmailAuthenticatorRepository, userOperationClaimRepository, mailService, otpAuthenticatorHelper, emailAuthenticatorHelper, otpAuthenticatorRepository, httpClient, configuration);
            AuthBussinessRules authBussinessRules = new AuthBussinessRules(siteUserRepository);


            this.verifyOtpAuthenticatorCommand = new VerifyOtpAuthenticatorCommand();
            this.validationRules = new VerifyOtpAuthenticatorCommandValidator();
            this.verifyOtpAuthenticatorCommandHandler = new VerifyOtpAuthenticatorCommandHandler(otpAuthenticatorRepository, authBussinessRules, authService);
        }
        [Fact]
        public async Task ThrowExceptionIfUserIDIsEmpty()
        {
            verifyOtpAuthenticatorCommand.OtpCode = "12345";
            TestValidationResult<VerifyOtpAuthenticatorCommand> testValidationResult = validationRules.TestValidate(verifyOtpAuthenticatorCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.UserId);
        }
        [Fact]
        public async Task ThrowExceptionIfOtpCodeIsEmpty()
        {
            verifyOtpAuthenticatorCommand.UserId = 12;
            TestValidationResult<VerifyOtpAuthenticatorCommand> testValidationResult = validationRules.TestValidate(verifyOtpAuthenticatorCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.OtpCode);
        }
        [Fact]
        public async Task UserOtpAuthenticatorShouldBeExists()
        {
            verifyOtpAuthenticatorCommand.OtpCode = "1234556";
            verifyOtpAuthenticatorCommand.UserId = 12;

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await verifyOtpAuthenticatorCommandHandler.Handle(verifyOtpAuthenticatorCommand, CancellationToken.None);
            });
        }
    }
}
