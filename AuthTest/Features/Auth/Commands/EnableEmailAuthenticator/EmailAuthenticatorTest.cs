using Application.Features.Auth.Commands.EnableEmailAuthenticator;
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
using static Application.Features.Auth.Commands.EnableEmailAuthenticator.EnableEmailAuthenticatorCommand;

namespace AuthTest.Features.Auth.Commands.EnableEmailAuthenticator
{
    public class EmailAuthenticatorTest : IClassFixture<Startup>
    {
        private readonly EnableEmailAuthenticatorCommandHandler enableEmailAuthenticatorCommandHandler;
        private readonly EnableEmailAuthenticatorCommand enableEmailAuthenticatorCommand;
        private readonly EnableEmailAuthenticatorCommandValidator validationRules;
        private readonly IConfiguration configuration;
        public EmailAuthenticatorTest(RefreshTokenFakeData refreshTokenFakeData,
            SiteUserFakeData siteUserFakeData, OperationClaimFakeData operationClaimFakeData, UserOperationClaimFakeData userOperationClaimFakeData, BanFakeData banFakeData,UserLoginFakeData userLoginFakeData)
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


            this.enableEmailAuthenticatorCommand = new EnableEmailAuthenticatorCommand();
            this.validationRules = new EnableEmailAuthenticatorCommandValidator();
            this.enableEmailAuthenticatorCommandHandler = new EnableEmailAuthenticatorCommandHandler(authService, mailService, userEmailAuthenticatorRepository, siteUserRepository, authBussinessRules);
        }
        [Fact]
        public async Task UserShouldBeExitst()
        {
            enableEmailAuthenticatorCommand.UserID = 234;
            enableEmailAuthenticatorCommand.VerifyToEmail = "example1@united.io";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await enableEmailAuthenticatorCommandHandler.Handle(enableEmailAuthenticatorCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task UserShoulBeHasAuthenticator()
        {
            enableEmailAuthenticatorCommand.UserID = 234;
            enableEmailAuthenticatorCommand.VerifyToEmail = "example1@united.io";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await enableEmailAuthenticatorCommandHandler.Handle(enableEmailAuthenticatorCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task ShouldThrowExceptionIfUserIDIsNull()
        {
            enableEmailAuthenticatorCommand.VerifyToEmail = "example1@united.io";

            TestValidationResult<EnableEmailAuthenticatorCommand> testValidationResult = validationRules.TestValidate(enableEmailAuthenticatorCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.UserID);

        }
    }
}
