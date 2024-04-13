using Application.Features.SiteUsers.Commands.CreateSiteUser;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using static Application.Features.SiteUsers.Commands.CreateSiteUser.CreateSiteUserCommand;

namespace AuthTest.Features.Users.Commands.Create
{
    public class CreateSiteUserTest : UserMockRepository
    {
        private readonly CreateSiteUserCommandValidator commandValidator;
        private readonly CreateSiteUserCommand createSiteUserCommand;
        private readonly CreateSiteUserCommandHandler createSiteUserCommandHandler;
        public CreateSiteUserTest(SiteUserFakeData siteUserFakeData, CreateSiteUserCommandValidator commandValidator, CreateSiteUserCommand createSiteUserCommand, CreateSiteUserCommandHandler createSiteUserCommandHandler) : base(siteUserFakeData)
        {
            this.commandValidator = commandValidator;
            this.createSiteUserCommand = createSiteUserCommand;
            this.createSiteUserCommandHandler = new CreateSiteUserCommandHandler(MockRepository.Object, Mapper, BusinessRules, MockMediator.Object);
        }

        [Fact]
        public void UserEmailNotMatchEmailRuleShouldReturnError()
        {
            #region Act
            createSiteUserCommand.FirstName = "First";
            createSiteUserCommand.LastName = "Last";
            createSiteUserCommand.Email = "NotEmailFormat";
            createSiteUserCommand.Password = "password";
            #endregion

            #region Arrange
            ValidationFailure? result = commandValidator.Validate(createSiteUserCommand).Errors.FirstOrDefault(x => x.PropertyName == "Email" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator);

            #endregion

            #region Assert

            Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);

            #endregion
        }
        [Fact]
        public async void CreateShouldSuccessfully()
        {
            #region Act
            createSiteUserCommand.FirstName = "First";
            createSiteUserCommand.LastName = "Last";
            createSiteUserCommand.Email = "test@email.com";
            createSiteUserCommand.Password = "password";
            #endregion

            #region Arrange
            CreateSiteUserResponse result = await createSiteUserCommandHandler.Handle(createSiteUserCommand, CancellationToken.None);
            #region 

            #region Assert

            Assert.Equal("test@email.com", result.Email);
            #endregion

        }
        [Fact]
        public async void DuplicatedUserEmailShouldReturnError()
        {
            #region Act
            createSiteUserCommand.FirstName = "First";
            createSiteUserCommand.LastName = "Last";
            createSiteUserCommand.Email = "test123@mailinator.com";
            createSiteUserCommand.Password = "password";
            #endregion

            #region Assert

            async Task Action() => await createSiteUserCommandHandler.Handle(createSiteUserCommand, CancellationToken.None);
            #endregion

            #region Assert
            Assert.ThrowsAsync<BusinessException>(Action);
            #region

        }
        [Fact]
        public async void PasswordRuleShouldMatchPasswordRuleReturnError()
        {
            #region Act
            createSiteUserCommand.FirstName = "First";
            createSiteUserCommand.LastName = "Last";
            createSiteUserCommand.Email = "NotEmailFormat";
            createSiteUserCommand.Password = "password";
            #endregion

            #region Assert

            ValidationFailure? result = commandValidator.
                Validate(createSiteUserCommand).Errors.FirstOrDefault(x => x.PropertyName == "Password" && x.ErrorCode == ValidationErrorCodes.MinimumLengthValidator);
            #region

            #region Assert
            Assert.Equal(ValidationErrorCodes.MinimumLengthValidator, result?.ErrorCode);
            #endregion
        }



    }
}
