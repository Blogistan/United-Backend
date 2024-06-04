using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Profiles;
using Application.Features.OperationClaims.Rules;
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
using static Application.Features.OperationClaims.Commands.CreateOperationClaim.CreateOperationClaimCommand;

namespace AuthTest.Features.OperationClaim.Commands.Create
{
    public class CreateOperationClaimTest : MockOperationClaimRepository, IClassFixture<Startup>
    {
        private readonly CreateOperationClaimCommand createOperationClaimCommand;
        private readonly CreateOperationClaimCommandHandler createOperationClaimCommandHandler;
        private readonly CreateOperationClaimCommandValidator validationRules;
        private readonly IConfiguration configuration;

        public CreateOperationClaimTest(RefreshTokenFakeData refreshTokenFakeData,
           SiteUserFakeData siteUserFakeData, OperationClaimFakeData operationClaimFakeData, UserOperationClaimFakeData userOperationClaimFakeData, BanFakeData banFakeData, ForgotPasswordFakeData forgotPasswordFakeData, IMediator mediator) : base(operationClaimFakeData)
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
            //IOperationClaimRepostiory operationClaimRepostiory = new MockOperationClaimRepository(operationClaimFakeData);
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
            OperationClaimBusinessRules operationClaimBusinessRules = new OperationClaimBusinessRules(MockRepository.Object);

            createOperationClaimCommand = new CreateOperationClaimCommand();
            validationRules = new CreateOperationClaimCommandValidator();
            createOperationClaimCommandHandler = new CreateOperationClaimCommandHandler(MockRepository.Object, mapper, operationClaimBusinessRules);

        }
        [Fact]
        public async Task ThrowExcepitonIfOperationClaimNameEmpty()
        {
            createOperationClaimCommand.Name = string.Empty;

            TestValidationResult<CreateOperationClaimCommand> testValidationResult = validationRules.TestValidate(createOperationClaimCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.Name);
        }
        [Fact]
        public async Task OperationClaimCannotBeDuplicatedWhenInserted()
        {
            createOperationClaimCommand.Name = "Moderator";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await createOperationClaimCommandHandler.Handle(createOperationClaimCommand, CancellationToken.None);
            });
        }


    }
}
