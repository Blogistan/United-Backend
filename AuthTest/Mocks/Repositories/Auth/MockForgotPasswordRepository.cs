using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace AuthTest.Mocks.Repositories.Auth
{
    public class MockForgotPasswordRepository
    {
        private readonly SiteUserFakeData siteUserFakeData;
        private readonly ForgotPasswordFakeData forgotPasswordFakeData;
        public MockForgotPasswordRepository(SiteUserFakeData siteUserFakeData, ForgotPasswordFakeData forgotPasswordFakeData)
        {
            this.siteUserFakeData = siteUserFakeData;
            this.forgotPasswordFakeData = forgotPasswordFakeData;
        }

        public IForgotPasswordRepository GetForgotPasswordRepository()
        {
            var mockRepo = new Mock<IForgotPasswordRepository>();

            mockRepo.Setup(s => s.GetAsync(
                    It.IsAny<Expression<Func<ForgotPassword, bool>>>(),
                    It.IsAny<Func<IQueryable<ForgotPassword>, IIncludableQueryable<ForgotPassword, object>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((
                    Expression<Func<ForgotPassword, bool>> predicate,
                    Func<IQueryable<ForgotPassword>, IIncludableQueryable<ForgotPassword, object>>? include,
                    bool withDeleted,
                    bool enableTracking,
                    CancellationToken cancellationToken
                    ) =>
                {
                    ForgotPassword forgotPassword = new ForgotPassword();

                    if (predicate != null)
                        forgotPassword = forgotPasswordFakeData.Data.Where(predicate.Compile()).FirstOrDefault();

                    if (forgotPassword != null)
                        forgotPassword.User = siteUserFakeData.Data.FirstOrDefault(x => x.Id == forgotPassword.UserId);

                    return forgotPassword;
                });

            return mockRepo.Object;
        }
    }
}
