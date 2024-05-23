using Application.Features.Auth.Commands.Register;
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
using MediatR;
using Microsoft.Extensions.Configuration;
using static Application.Features.Auth.Commands.Register.RegisterCommand;

namespace AuthTest.Features.Auth.Commands.Register
{
    public class RegisterTest : IClassFixture<Startup>
    {
        private readonly RegisterCommand registerCommand;
        private readonly RegisterCommandHandler registerCommandHandler;
        private readonly RegisterCommandValidator validationRules;
        private readonly IConfiguration configuration;
        private readonly IMediator mediator;

        public RegisterTest(RefreshTokenFakeData refreshTokenFakeData,
           SiteUserFakeData siteUserFakeData, OperationClaimFakeData operationClaimFakeData, UserOperationClaimFakeData userOperationClaimFakeData, BanFakeData banFakeData, IMediator mediator)
        {

            #region Mock Repositories
            this.configuration = MockConfiguration.GetMockConfiguration();
            IUserOperationClaimRepository userOperationClaimRepository = new MockUserOperationClaimRepository(userOperationClaimFakeData, operationClaimFakeData).GetOperationClaimRepostiory();
            IRefreshTokenRepository refreshTokenRepository = new MockRefreshTokenRepository(refreshTokenFakeData).GetRefreshTokenRepository();
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
            this.registerCommand = new RegisterCommand();
            this.validationRules = new RegisterCommandValidator();
            this.registerCommandHandler = new RegisterCommandHandler(siteUserRepository, authBussinessRules, authService, mapper, mediator);

        }
        [Fact]
        public async void RegisterWithExistsUserShouldThrownException()
        {
            registerCommand.UserForRegisterDto = new() { Email = "example@united.io", FirstName = "test", LastName = "test", Password = "123456" };

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
               await registerCommandHandler.Handle(registerCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async void RegisterWithLessThanSixPasswordCharacters()
        {
            registerCommand.UserForRegisterDto = new() { Email = "example123123@united.io", FirstName = "test", LastName = "test", Password = "123" };

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await registerCommandHandler.Handle(registerCommand, CancellationToken.None);
            });
        }


    }
}
