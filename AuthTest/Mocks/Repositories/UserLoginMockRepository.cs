using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace AuthTest.Mocks.Repositories
{
    public class MockUserLoginRepository 
    {
        public static void SetupGetListAsync(Mock<IUserLoginRepository> mockRepo, UserLoginFakeData banFakeData)
       =>

           mockRepo.Setup(s => s.GetListAsync(
           It.IsAny<Expression<Func<UserLogin, bool>>>(),
           It.IsAny<Func<IQueryable<UserLogin>, IOrderedQueryable<UserLogin>>>(),
           It.IsAny<Func<IQueryable<UserLogin>, IIncludableQueryable<UserLogin, object>>>(),
           It.IsAny<int>(),
           It.IsAny<int>(),
           It.IsAny<bool>(),
           It.IsAny<bool>(),
           It.IsAny<CancellationToken>()
               )).ReturnsAsync((
           Expression<Func<UserLogin, bool>>? predicate,
           Func<IQueryable<UserLogin>, IOrderedQueryable<UserLogin>>? orderBy,
           Func<IQueryable<UserLogin>, IIncludableQueryable<UserLogin, object>>? include,
           int index,
           int size,
           bool withDeleted,
           bool enableTracking,
           CancellationToken cancellationToken
               ) =>
               {
                   var query = banFakeData.Data.AsQueryable();

                   if (predicate != null)
                       query.Where(predicate.Compile());

                   


                   var banResult = query.ToList();

                   var paginateResult = new Paginate<UserLogin>
                   {
                       Items = banResult,
                       Index = index,
                       Count = banResult.Count,
                       From = index * size,
                       Size = size
                   };

                   return paginateResult;

               });


        public static void SetupAddAsync(Mock<IUserLoginRepository> mockRepo, UserLoginFakeData userLoginFakeData) =>
            mockRepo.Setup(s => s.AddAsync(It.IsAny<UserLogin>()))
            .ReturnsAsync((UserLogin userLogin) =>
            {
                userLoginFakeData.Data.Add(userLogin);
                return userLogin;
            });

        public static void SetupUpdateAsync(Mock<IUserLoginRepository> mockRepo, UserLoginFakeData userLoginFakeData) =>
            mockRepo.Setup(s => s.UpdateAsync(It.IsAny<UserLogin>()))
            .ReturnsAsync((UserLogin userLogin) =>
            {
                UserLogin? result = userLoginFakeData.Data.FirstOrDefault(x => x.Id!.Equals(userLogin.Id));
                if (result != null)
                    result = userLogin;
                return result;
            });

        public static void SetupDeleteAsync(Mock<IUserLoginRepository> mockRepo, UserLoginFakeData userLoginFakeData) =>
            mockRepo.Setup(s => s.DeleteAsync(It.IsAny<UserLogin>(), It.IsAny<bool>()))
            .ReturnsAsync((UserLogin userLogin, bool permanent) =>
            {
                if (!permanent)
                {

                    userLogin.DeletedDate = DateTime.UtcNow;
                }
                else
                    userLoginFakeData.Data.Remove(userLogin);
                return userLogin;
            });


        public static void Build(Mock<IUserLoginRepository> mockRepo, UserLoginFakeData userLoginFakeData)
        {
            SetupGetListAsync(mockRepo, userLoginFakeData);
            SetupAddAsync(mockRepo, userLoginFakeData);
            SetupUpdateAsync(mockRepo, userLoginFakeData);
            SetupDeleteAsync(mockRepo, userLoginFakeData);
        }

        public static Mock<IUserLoginRepository> GetUserLoginRepository(UserLoginFakeData userLoginFakeData)
        {
            var mockRepo = new Mock<IUserLoginRepository>();

            Build(mockRepo, userLoginFakeData);

            return mockRepo;
        }
    }
}
