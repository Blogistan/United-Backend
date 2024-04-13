using Application.Features.SiteUsers.Commands.UpdateSiteUser;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using static Application.Features.SiteUsers.Commands.UpdateSiteUser.UpdateSiteUserCommand;

namespace AuthTest.Features.Users.Commands.Update
{
    public class UpdateSiteUserTest : UserMockRepository
    {
        private readonly UpdateSiteUserCommandValidator validator;
        private readonly UpdateSiteUserCommand updateSiteUserCommand;
        private readonly UpdateSiteUserCommandHandler updateSiteUserCommandHandler;

        public UpdateSiteUserTest(SiteUserFakeData siteUserFakeData, UpdateSiteUserCommandValidator validator, UpdateSiteUserCommand updateSiteUserCommand, UpdateSiteUserCommandHandler updateSiteUserCommandHandler) : base(siteUserFakeData)
        {
            this.validator = validator;
            this.updateSiteUserCommand = updateSiteUserCommand;
            this.updateSiteUserCommandHandler = updateSiteUserCommandHandler;
        }
        [Fact]
        public void UserEmailNotMatchEmailRuleShouldReturnError()
        {
            #region Act
            updateSiteUserCommand.Email = "NotEmailFormat";
            #endregion

            #region Arrange

            ValidationFailure? result = validator.Validate(updateSiteUserCommand).Errors.FirstOrDefault(x => x.PropertyName == "Email" && x.ErrorCode == ValidationErrorCodes.EmailValidator);

            #endregion

            #region Assert
            Assert.Equal(ValidationErrorCodes.EmailValidator, result.ErrorCode);
            #endregion
        }
        [Fact]
        public void UserEmailEmptyShouldReturnError()
        {
            #region Act
            updateSiteUserCommand.Email = string.Empty;
            #endregion

            #region Arrange
            ValidationFailure? result = validator.Validate(updateSiteUserCommand).Errors.FirstOrDefault(x => x.PropertyName == "Email" && x.ErrorCode == ValidationErrorCodes.NotEmptyValidator);
            #endregion

            #region Assert
            Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
            #endregion
        }
        [Fact]
        public async void UpdateShouldSuccessfully()
        {
            #region Act
            updateSiteUserCommand.Id = SiteUserFakeData.Ids[0];
            updateSiteUserCommand.FirstName = "F";
            updateSiteUserCommand.LastName = "L";
            updateSiteUserCommand.Email = "test@email.com";
            updateSiteUserCommand.Password = "password";
            #endregion

            #region Arrange
            UpdateSiteUserCommandResponse result = await updateSiteUserCommandHandler.Handle(updateSiteUserCommand, CancellationToken.None);
            #endregion

            #region Assert
            Assert.Equal("test@email.com", result.Email);
            #endregion
        }
        [Fact]
        public async Task UserIdNotExistsShouldReturnError()
        {
            #region Act
            updateSiteUserCommand.Id = SiteUserFakeData.Ids[0];
            updateSiteUserCommand.FirstName = "F";
            updateSiteUserCommand.LastName = "L";
            updateSiteUserCommand.Email = "test@email.com";
            updateSiteUserCommand.Password = "password";
            #endregion

            #region Arrange

            async Task Action() => await updateSiteUserCommandHandler.Handle(updateSiteUserCommand, CancellationToken.None);
            #endregion

            #region Assert
            await Assert.ThrowsAsync<BusinessException>(Action);
            #endregion
        }
    }
}
