using Application.Features.SiteUsers.Commands.DeleteSiteUser;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using static Application.Features.SiteUsers.Commands.DeleteSiteUser.DeleteSiteUserCommand;

namespace AuthTest.Features.Users.Commands.Delete
{
    public class DeleteSiteUserTest : UserMockRepository,IClassFixture<Startup>
    {
        private readonly DeleteSiteUserCommandHandler deleteSiteUserCommandHandler;
        private readonly DeleteSiteUserCommand deleteSiteUserCommand;
        public DeleteSiteUserTest(SiteUserFakeData userFakeData,DeleteSiteUserCommand deleteSiteUserCommand) : base(userFakeData)
        {
            this.deleteSiteUserCommandHandler = new DeleteSiteUserCommandHandler(MockRepository.Object, Mapper, BusinessRules);
            this.deleteSiteUserCommand = deleteSiteUserCommand;
        }
        [Fact]
        public async Task DeleteShouldSuccessfully()
        {
            #region Act
            deleteSiteUserCommand.Id = SiteUserFakeData.Ids[0];
            #endregion

            #region Arrange
            DeleteSiteUserCommandResponse result = await deleteSiteUserCommandHandler.Handle(deleteSiteUserCommand, CancellationToken.None);
            #endregion

            #region Assert
            Assert.NotNull(result);
            #endregion
        }
        [Fact]
        public async Task UserIdNotExistsShouldReturnError()
        {
            #region Act
            deleteSiteUserCommand.Id = SiteUserFakeData.Ids[0];
            #endregion


            #region Arrange
            async Task Action() => await deleteSiteUserCommandHandler.Handle(deleteSiteUserCommand, CancellationToken.None);
            #endregion

            #region Assert
            await Assert.ThrowsAsync<BusinessException>(Action);
            #endregion

        }
    }
}
