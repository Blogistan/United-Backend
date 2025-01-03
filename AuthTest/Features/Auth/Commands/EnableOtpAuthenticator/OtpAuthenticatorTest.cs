﻿using Application.Features.Auth.Commands.EnableOtpAuthenticatorCommand;
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
using static Application.Features.Auth.Commands.EnableOtpAuthenticatorCommand.EnableOtpAuthenticatorCommand;

namespace AuthTest.Features.Auth.Commands.EnableOtpAuthenticator
{
    public class OtpAuthenticatorTest : IClassFixture<Startup>
    {
        private readonly EnableOtpAuthenticatorCommand enableOtpAuthenticatorCommand;
        private readonly EnableOtpAuthenticatorCommandHandler enableOtpAuthenticatorCommandHandler;
        private readonly EnableOtpAuthenticatorCommandValidator validationRules;
        private readonly IConfiguration configuration;
        public OtpAuthenticatorTest(RefreshTokenFakeData refreshTokenFakeData,
            SiteUserFakeData siteUserFakeData, OperationClaimFakeData operationClaimFakeData, UserOperationClaimFakeData userOperationClaimFakeData, BanFakeData banFakeData, OtpAuthenticatorFakeData otpAuthenticatorFakeData,UserLoginFakeData userLoginFakeData, UserFakeData userFakeData)
        {

            #region Mock Repositories
            this.configuration = MockConfiguration.GetMockConfiguration();
            IUserOperationClaimRepository userOperationClaimRepository = new MockUserOperationClaimRepository(userOperationClaimFakeData, operationClaimFakeData).GetOperationClaimRepostiory();
            IRefreshTokenRepository refreshTokenRepository = new MockRefreshTokenRepository(refreshTokenFakeData, siteUserFakeData).GetRefreshTokenRepository();
            IEmailAuthenticatorRepository userEmailAuthenticatorRepository =
            MockEmailAuthenticatorRepository.GetEmailAuthenticatorRepositoryMock();
            IOtpAuthenticatorRepository otpAuthenticatorRepository = MockOtpAuthRepository.GetOtpAuthenticatorRepository();
            ISiteUserRepository siteUserRepository = new MockSiteUserRepository(siteUserFakeData, banFakeData).GetSiteUserRepository();
            IUserRepository userRepository = new MockUserRepository(userFakeData).GetUserRepository();
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
            AuthBussinessRules authBussinessRules = new AuthBussinessRules(userRepository, siteUserRepository);
            IAuthService authService = new AuthService(tokenHelper, refreshTokenRepository, siteUserRepository, userEmailAuthenticatorRepository, userOperationClaimRepository, mailService, otpAuthenticatorHelper, emailAuthenticatorHelper, otpAuthenticatorRepository, httpClient, configuration,userLoginRepository, authBussinessRules);
            //AuthBussinessRules authBussinessRules = new AuthBussinessRules(siteUserRepository);


            this.enableOtpAuthenticatorCommand = new EnableOtpAuthenticatorCommand();
            this.validationRules = new EnableOtpAuthenticatorCommandValidator();
            //this.enableOtpAuthenticatorCommandHandler = new EnableOtpAuthenticatorCommandHandler(authBussinessRules, otpAuthenticatorRepository, siteUserRepository, authService);
        }
        [Fact]
        public async Task UserShouldBeExists()
        {
            enableOtpAuthenticatorCommand.UserID = 1;

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await enableOtpAuthenticatorCommandHandler.Handle(enableOtpAuthenticatorCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task UserShouldNotBeHasAuthenticator()
        {
            enableOtpAuthenticatorCommand.UserID = 234;

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await enableOtpAuthenticatorCommandHandler.Handle(enableOtpAuthenticatorCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task SecretKeyShouldNotEmpty()
        {
            enableOtpAuthenticatorCommand.UserID = 234;
            enableOtpAuthenticatorCommand.SecretKeyLabel = "Test";
            enableOtpAuthenticatorCommand.SecretKeyIssuer = "TestIssuer";

            EnabledOtpAuthenticatorResponse enabledOtpAuthenticatorResponse = await enableOtpAuthenticatorCommandHandler.Handle(enableOtpAuthenticatorCommand, CancellationToken.None);

            Assert.NotEmpty(enabledOtpAuthenticatorResponse.SecretKey);
        }
        [Fact]
        public async Task ThrowExcepitonIfUserHasAuthenticator()
        {
            enableOtpAuthenticatorCommand.UserID = 12;
            enableOtpAuthenticatorCommand.SecretKeyLabel = "Test";
            enableOtpAuthenticatorCommand.SecretKeyIssuer = "TestIssuer";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await enableOtpAuthenticatorCommandHandler.Handle(enableOtpAuthenticatorCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task ThrowExceptionIfUserIDIsNull()
        {
            enableOtpAuthenticatorCommand.SecretKeyLabel = "Test";
            enableOtpAuthenticatorCommand.SecretKeyIssuer = "TestIssuer";

            TestValidationResult<EnableOtpAuthenticatorCommand> testValidationResult = validationRules.TestValidate(enableOtpAuthenticatorCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.UserID);
        }

    }
}
