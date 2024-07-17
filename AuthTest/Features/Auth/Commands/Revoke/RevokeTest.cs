using Application.Features.Auth.Commands.Revoke;
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
using MediatR;
using Microsoft.Extensions.Configuration;
using static Application.Features.Auth.Commands.Revoke.RevokeCommand;

namespace AuthTest.Features.Auth.Commands.Revoke
{
    public class RevokeTest:IClassFixture<Startup>
    {
        private readonly RevokeCommand revokeCommand;
        private readonly RevokeCommandHandler revokeCommandHandler;
        private readonly RevokeCommandValidator validationRules;
        private readonly IConfiguration configuration;
        private readonly IMediator mediator;

        public RevokeTest(RefreshTokenFakeData refreshTokenFakeData,
           SiteUserFakeData siteUserFakeData, OperationClaimFakeData operationClaimFakeData, UserOperationClaimFakeData userOperationClaimFakeData, BanFakeData banFakeData, IMediator mediator, UserLoginFakeData userLoginFakeData)
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
            IAuthService authService = new AuthService(tokenHelper, refreshTokenRepository, siteUserRepository, userEmailAuthenticatorRepository, userOperationClaimRepository, mailService, otpAuthenticatorHelper, emailAuthenticatorHelper, otpAuthenticatorRepository, httpClient, configuration,userLoginRepository);
            AuthBussinessRules authBussinessRules = new AuthBussinessRules(siteUserRepository);

            this.mediator = mediator;
            this.revokeCommand = new RevokeCommand();
            this.validationRules = new RevokeCommandValidator();
            this.revokeCommandHandler = new RevokeCommandHandler(refreshTokenRepository,authBussinessRules,authService,mapper);

        }
        [Fact]
        public async Task RefreshTokenShouldBeExists()
        {
            revokeCommand.Token = "ASDASDASD1";
            revokeCommand.IpAddress = "::0";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await revokeCommandHandler.Handle(revokeCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task RefreshTokenShouldBeActive()
        {
            revokeCommand.Token = "abc12";
            revokeCommand.IpAddress = "::0";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await revokeCommandHandler.Handle(revokeCommand, CancellationToken.None);
            });
        }
    }
}
