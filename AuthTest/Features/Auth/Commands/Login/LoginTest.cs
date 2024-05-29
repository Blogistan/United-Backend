using Application.Features.Auth.Commands.Login;
using Application.Services.Repositories;
using AuthTest.Mocks.Configurations;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories.Auth;
using Core.Mailing.MailKitImplementations;
using Core.Mailing;
using Core.Security.EmailAuthenticator;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator.OtpNet;
using Core.Security.OtpAuthenticator;
using Microsoft.Extensions.Configuration;
using static Application.Features.Auth.Commands.Login.LoginCommand;
using AutoMapper;
using Application.Features.Auth.Profiles;
using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;

namespace AuthTest.Features.Auth.Commands.Login
{
    public class LoginTest : IClassFixture<Startup>
    {
        private readonly LoginCommand loginCommand;
        private readonly LoginCommandHandler loginCommandHandler;
        private readonly LoginCommandValidator validationRules;
        private readonly IConfiguration configuration;
        public LoginTest(RefreshTokenFakeData refreshTokenFakeData,
            SiteUserFakeData siteUserFakeData, OperationClaimFakeData operationClaimFakeData, UserOperationClaimFakeData userOperationClaimFakeData, BanFakeData banFakeData)
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


            this.loginCommand = new LoginCommand();
            this.validationRules = new LoginCommandValidator();
            this.loginCommandHandler = new LoginCommandHandler(authService, authBussinessRules, siteUserRepository);


        }
        [Fact]
        public async Task SuccessfullLoginShouldReturnAccessToken()
        {
            // loginCommand.UserForLoginDto = new() { Email = "string@mailinator.com", Password = "string123" };
            loginCommand.UserForLoginDto = new() { Email = "example2@united.io", Password = "123456" };

            LoginResponse loginResponse = await loginCommandHandler.Handle(loginCommand, CancellationToken.None);

            Assert.NotNull(loginResponse.AccessToken.Token);
        }
        [Fact]
        public async Task AccessTokenShouldHaveValidExpirationTime()
        {
            loginCommand.UserForLoginDto = new() { Email = "example2@united.io", Password = "123456" };
            LoginResponse loginResponse = await loginCommandHandler.Handle(loginCommand, CancellationToken.None);

            TokenOptions? tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            bool tokenExpiresInTime = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration + 1) > loginResponse.AccessToken.Expiration;
            Assert.True(tokenExpiresInTime, "Access token expiration time is invalid.");
        }
        [Fact]
        public async Task LoginWithWrongPasswordShouldThrowException()
        {
            loginCommand.UserForLoginDto = new() { Email = "example2@united.io", Password = "48946985146584" };
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await loginCommandHandler.Handle(loginCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task LoginWithWrongEmailShouldThrowException()
        {
            loginCommand.UserForLoginDto = new() { Email = "example213123@united.io", Password = "123456" };
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await loginCommandHandler.Handle(loginCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task LoginWithInvalidLengthPasswordShouldThrowException()
        {
            loginCommand.UserForLoginDto = new() { Email = "example2@united.io", Password = "1" };
            TestValidationResult<LoginCommand> testValidationResult = validationRules.TestValidate(loginCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.UserForLoginDto.Password);
        }
        [Fact]
        public async Task LoginWithNullPasswordShouldThrowException()
        {
            loginCommand.UserForLoginDto = new() { Email = "example2@united.io", Password = null! };
            TestValidationResult<LoginCommand> testValidationResult = validationRules.TestValidate(loginCommand);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.UserForLoginDto.Password);
        }
        [Fact]
        public async Task LoginWithBannedUserShouldThrownException()
        {
            // loginCommand.UserForLoginDto = new() { Email = "string@mailinator.com", Password = "string123" };
            loginCommand.UserForLoginDto = new() { Email = "example2@united.io", Password = "123456" };

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                LoginResponse loginResponse = await loginCommandHandler.Handle(loginCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task LoginWithtTimeOutedUserShouldThrownException()
        {
            loginCommand.UserForLoginDto = new() { Email = "example@united.io", Password = "123456" };
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                LoginResponse loginResponse = await loginCommandHandler.Handle(loginCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task LoginWithInvlaidEmailShouldThrowException()
        {
            loginCommand.UserForLoginDto = new() { Email = "example.united", Password = "123456" };

            TestValidationResult<LoginCommand> testValidationResult = validationRules.TestValidate(loginCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.UserForLoginDto.Email);
        }

    }
}

