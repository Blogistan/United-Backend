using Application.Features.Auth.Commands.Refresh;
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
using MediatR;
using Microsoft.Extensions.Configuration;
using static Application.Features.Auth.Commands.Refresh.RefreshCommand;

namespace AuthTest.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenTest : IClassFixture<Startup>
    {
        private readonly RefreshCommand refreshCommand;
        private readonly RefreshCommandHandler refreshCommandHandler;
        private readonly RefreshCommandValidator validationRules;
        private readonly IConfiguration configuration;
        private readonly IMediator mediator;

        public RefreshTokenTest(RefreshTokenFakeData refreshTokenFakeData,
           SiteUserFakeData siteUserFakeData, OperationClaimFakeData operationClaimFakeData, UserOperationClaimFakeData userOperationClaimFakeData, BanFakeData banFakeData, IMediator mediator)
        {

            #region Mock Repositories
            this.configuration = MockConfiguration.GetMockConfiguration();
            IUserOperationClaimRepository userOperationClaimRepository = new MockUserOperationClaimRepository(userOperationClaimFakeData, operationClaimFakeData).GetOperationClaimRepostiory();
            IRefreshTokenRepository refreshTokenRepository = new MockRefreshTokenRepository(refreshTokenFakeData,siteUserFakeData).GetRefreshTokenRepository();
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

            this.mediator = mediator;
            this.refreshCommand = new RefreshCommand();
            this.validationRules = new RefreshCommandValidator();
            this.refreshCommandHandler = new RefreshCommandHandler(siteUserRepository, authService, refreshTokenRepository, authBussinessRules);

        }
        [Fact]
        public async Task RefreshTokenShouldBeExists()
        {
            refreshCommand.RefreshToken = "ASDASDASD1";
            refreshCommand.IpAddress = "::0";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await refreshCommandHandler.Handle(refreshCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task IsTokenOrIpAdressNullThenThrowException()
        {
            refreshCommand.RefreshToken = string.Empty;
            refreshCommand.IpAddress = string.Empty;

            TestValidationResult<RefreshCommand> testValidationResult = validationRules.TestValidate<RefreshCommand>(refreshCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.RefreshToken);
        }
        [Fact]
        public async Task AccessTokenShouldHaveValidExpirationTime()
        {
            refreshCommand.RefreshToken = "abc";
            refreshCommand.IpAddress = "::0";
            RefreshedResponse result = await refreshCommandHandler.Handle(refreshCommand, CancellationToken.None);


            TokenOptions? tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
            bool tokenExpiresInTime = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration + 1) > result.AccessToken.Expiration;
            Assert.True(tokenExpiresInTime, "Access token expiration time is invalid.");
        }

    }
}
