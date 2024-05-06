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

namespace AuthTest.Features.Auth.Commands.Login
{
    public class LoginTest : IClassFixture<Startup>
    {
        private readonly LoginCommand loginCommand;
        private readonly LoginCommandHandler loginCommandHandler;
        private readonly LoginCommandValidator validationRules;
        private readonly IConfiguration configuration;
        public LoginTest(LoginCommand loginCommand, LoginCommandHandler loginCommandHandler, LoginCommandValidator validationRules, IConfiguration configuration, RefreshTokenFakeData refreshTokenFakeData,
            SiteUserFakeData siteUserFakeData, OperationClaimFakeData operationClaimFakeData)
        {
            
            #region Mock Repositories

            IUserOperationClaimRepository userOperationClaimRepository = new MockUserOperationClaimRepository(operationClaimFakeData).GetOperationClaimRepostiory();
            IRefreshTokenRepository refreshTokenRepository = new MockRefreshTokenRepository(refreshTokenFakeData).GetRefreshTokenRepository();
            IEmailAuthenticatorRepository userEmailAuthenticatorRepository =
            MockEmailAuthenticatorRepository.GetEmailAuthenticatorRepositoryMock();
            IOtpAuthenticatorRepository otpAuthenticatorRepository = MockOtpAuthRepository.GetOtpAuthenticatorRepository();
            ISiteUserRepository siteUserRepository = new MockUserRepository(siteUserFakeData).GetSiteUserRepository();
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
            IAuthService authService = new AuthService(tokenHelper,refreshTokenRepository,siteUserRepository,userEmailAuthenticatorRepository,userOperationClaimRepository,mailService,otpAuthenticatorHelper,emailAuthenticatorHelper,otpAuthenticatorRepository,httpClient,configuration);
            AuthBussinessRules authBussinessRules = new AuthBussinessRules(siteUserRepository);

            this.loginCommand = new LoginCommand();
            this.loginCommandHandler = new LoginCommandHandler(authService,authBussinessRules,siteUserRepository);
            this.validationRules = new LoginCommandValidator();
            this.configuration = MockConfiguration.GetMockConfiguration();

        }

    }
}

