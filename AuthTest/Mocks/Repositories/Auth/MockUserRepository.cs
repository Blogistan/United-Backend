using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace AuthTest.Mocks.Repositories.Auth
{
    public class MockUserRepository
    {

        private readonly UserFakeData userFakeData;
        public MockUserRepository(UserFakeData userFakeData)
        {
            this.userFakeData = userFakeData;
        }
        public IUserRepository GetUserRepository()
        {
            var mockRepo = new Mock<IUserRepository>();

            mockRepo.Setup(s => s.GetAsync(
                    It.IsAny<Expression<Func<User, bool>>>(),
                    It.IsAny<Func<IQueryable<User>, IIncludableQueryable<User, object>>>(),
                    It.IsAny<bool>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()
                ))
                .ReturnsAsync((
                    Expression<Func<User, bool>> predicate,
                    Func<IQueryable<User>, IIncludableQueryable<User, object>>? include,
                    bool withDeleted,
                    bool enableTracking,
                    CancellationToken cancellationToken
                    ) =>
                {
                    User user = new User();

                    if (predicate != null)
                        user = userFakeData.Data.Where(predicate.Compile()).FirstOrDefault();

                    return user;
                });

            return mockRepo.Object;
        }

    }
}
