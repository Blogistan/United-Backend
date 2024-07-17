using Application.Features.OperationClaims.Profiles;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using AuthTest.Mocks.Configurations;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories.Auth;
using AutoMapper;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using Core.Security.EmailAuthenticator;
using Core.Security.JWT;
using Core.Security.OtpAuthenticator;
using Core.Security.OtpAuthenticator.OtpNet;
using MediatR;
using Microsoft.Extensions.Configuration;
using static Application.Features.OperationClaims.Commands.DeleteOperationClaim.DeleteOperationClaimCommand;
using FluentValidation.TestHelper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using AuthTest.Mocks.Repositories;

namespace AuthTest.Features.OperationClaim.Commands.Delete
{
    public class DeleteOperationClaimTest : MockOperationClaimRepository,IClassFixture<Startup>
    {
        private readonly DeleteOperationClaimCommand deleteOperationClaimCommand;
        private readonly DeleteOperationClaimCommandHandler deleteOperationClaimCommandHandler;
        private readonly DeleteOperationClaimCommandValidator validationRules;
        private readonly IConfiguration configuration;

        public DeleteOperationClaimTest(RefreshTokenFakeData refreshTokenFakeData,
           SiteUserFakeData siteUserFakeData, OperationClaimFakeData operationClaimFakeData, UserOperationClaimFakeData userOperationClaimFakeData, BanFakeData banFakeData, ForgotPasswordFakeData forgotPasswordFakeData, IMediator mediator,UserLoginFakeData userLoginFakeData):base(operationClaimFakeData)
        {

            #region Mock Repositories
            configuration = MockConfiguration.GetMockConfiguration();
            IUserOperationClaimRepository userOperationClaimRepository = new MockUserOperationClaimRepository(userOperationClaimFakeData, operationClaimFakeData).GetOperationClaimRepostiory();
            IRefreshTokenRepository refreshTokenRepository = new MockRefreshTokenRepository(refreshTokenFakeData, siteUserFakeData).GetRefreshTokenRepository();
            IEmailAuthenticatorRepository userEmailAuthenticatorRepository =
            MockEmailAuthenticatorRepository.GetEmailAuthenticatorRepositoryMock();
            IOtpAuthenticatorRepository otpAuthenticatorRepository = MockOtpAuthRepository.GetOtpAuthenticatorRepository();
            ISiteUserRepository siteUserRepository = new MockUserRepository(siteUserFakeData, banFakeData).GetSiteUserRepository();
            IForgotPasswordRepository forgotPasswordRepository = new MockForgotPasswordRepository(siteUserFakeData, forgotPasswordFakeData).GetForgotPasswordRepository();
            //IOperationClaimRepostiory operationClaimRepostiory = new MockOperationClaimRepository(operationClaimFakeData).GetOperationClaimRepostiory();
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
            OperationClaimBusinessRules operationClaimBusinessRules = new OperationClaimBusinessRules(MockRepository.Object);

            deleteOperationClaimCommand = new DeleteOperationClaimCommand();
            validationRules = new DeleteOperationClaimCommandValidator();
            deleteOperationClaimCommandHandler = new DeleteOperationClaimCommandHandler(MockRepository.Object, mapper, operationClaimBusinessRules);

        }
        [Fact]
        public async Task IdShouldNotEmpty()
        {
            TestValidationResult<DeleteOperationClaimCommand> testValidationResult = validationRules.TestValidate(deleteOperationClaimCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Fact]
        public async Task OperationClaimCheckById()
        {
            deleteOperationClaimCommand.Id = 95;
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await deleteOperationClaimCommandHandler.Handle(deleteOperationClaimCommand, CancellationToken.None);
            });
        }



    }
}
