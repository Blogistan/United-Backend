﻿using Application.Features.Auth.Rules;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.OperationClaims.Profiles;
using Application.Features.OperationClaims.Rules;
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
using MediatR;
using Microsoft.Extensions.Configuration;
using static Application.Features.OperationClaims.Commands.UpdateOperationClaim.UpdateOperationClaimCommand;

namespace AuthTest.Features.OperationClaim.Commands.Update
{
    public class UpdateOperationClaimTest : MockOperationClaimRepository,IClassFixture<Startup>
    {
        private readonly UpdateOperationClaimCommand updateOperationClaimCommand;
        private readonly UpdateOperationClaimCommandHandler updateOperationClaimCommandHandler;
        private readonly UpdateOperationClaimCommandValidator validationRules;
        private readonly IConfiguration configuration;

        public UpdateOperationClaimTest(RefreshTokenFakeData refreshTokenFakeData,
           SiteUserFakeData siteUserFakeData, OperationClaimFakeData operationClaimFakeData, UserOperationClaimFakeData userOperationClaimFakeData, BanFakeData banFakeData, ForgotPasswordFakeData forgotPasswordFakeData, IMediator mediator, UserLoginFakeData userLoginFakeData, UserFakeData userFakeData) :base(operationClaimFakeData)
        {

            #region Mock Repositories
            configuration = MockConfiguration.GetMockConfiguration();
            IUserOperationClaimRepository userOperationClaimRepository = new MockUserOperationClaimRepository(userOperationClaimFakeData, operationClaimFakeData).GetOperationClaimRepostiory();
            IRefreshTokenRepository refreshTokenRepository = new MockRefreshTokenRepository(refreshTokenFakeData, siteUserFakeData).GetRefreshTokenRepository();
            IEmailAuthenticatorRepository userEmailAuthenticatorRepository =
            MockEmailAuthenticatorRepository.GetEmailAuthenticatorRepositoryMock();
            IOtpAuthenticatorRepository otpAuthenticatorRepository = MockOtpAuthRepository.GetOtpAuthenticatorRepository();
            ISiteUserRepository siteUserRepository = new MockSiteUserRepository(siteUserFakeData, banFakeData).GetSiteUserRepository();
            IUserRepository userRepository = new MockUserRepository(userFakeData).GetUserRepository();
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
            AuthBussinessRules authBussinessRules = new AuthBussinessRules(userRepository, siteUserRepository);
            IAuthService authService = new AuthService(tokenHelper, refreshTokenRepository, siteUserRepository, userEmailAuthenticatorRepository, userOperationClaimRepository, mailService, otpAuthenticatorHelper, emailAuthenticatorHelper, otpAuthenticatorRepository, httpClient, configuration, userLoginRepository, authBussinessRules);
            OperationClaimBusinessRules operationClaimBusinessRules = new OperationClaimBusinessRules(MockRepository.Object);

            updateOperationClaimCommand = new UpdateOperationClaimCommand();
            validationRules = new UpdateOperationClaimCommandValidator();
            updateOperationClaimCommandHandler = new UpdateOperationClaimCommandHandler(MockRepository.Object, mapper, operationClaimBusinessRules);

        }
        [Fact]
        public async Task OperationClaimIDShouldNotEmpty()
        {
            updateOperationClaimCommand.Name = "TEST";
            TestValidationResult<UpdateOperationClaimCommand> testValidationResult = validationRules.TestValidate(updateOperationClaimCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.Id);
        }
        [Fact]
        public async Task OperationClaimNameShouldNotEmpty()
        {
            updateOperationClaimCommand.Id = 5465;
            TestValidationResult<UpdateOperationClaimCommand> testValidationResult = validationRules.TestValidate(updateOperationClaimCommand);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.Name);
        }
        [Fact]
        public async Task OperationClaimCheckById()
        {
            updateOperationClaimCommand.Id = 5465;

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await updateOperationClaimCommandHandler.Handle(updateOperationClaimCommand, CancellationToken.None);
            });
        }
        [Fact]
        public async Task BlogCannotBeDuplicatedWhenUpdated()
        {
            updateOperationClaimCommand.Name = "Moderator";

            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await updateOperationClaimCommandHandler.Handle(updateOperationClaimCommand, CancellationToken.None);
            });
        }
    }
}
