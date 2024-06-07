using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace AuthTest.Mocks.Repositories
{
    public static class BanMockRepository
    {
      
        public static void SetupGetListAsync(Mock<IBanRepository> mockRepo, BanFakeData banFakeData, SiteUserFakeData siteUserFakeData, ReportFakeData reportFakeData)
        =>

            mockRepo.Setup(s => s.GetListAsync(
            It.IsAny<Expression<Func<Ban, bool>>>(),
            It.IsAny<Func<IQueryable<Ban>, IOrderedQueryable<Ban>>>(),
            It.IsAny<Func<IQueryable<Ban>, IIncludableQueryable<Ban, object>>>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<bool>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()
                )).ReturnsAsync((
            Expression<Func<Ban, bool>>? predicate,
            Func<IQueryable<Ban>, IOrderedQueryable<Ban>>? orderBy,
            Func<IQueryable<Ban>, IIncludableQueryable<Ban, object>>? include,
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

                    if (include != null)
                    {
                        foreach (var item in banFakeData.Data)
                        {
                            item.User = siteUserFakeData.Data.FirstOrDefault(s => s.Id == item.UserID);
                        }

                        foreach (var item in banFakeData.Data)
                        {
                            item.Report = reportFakeData.Data.LastOrDefault(r => r.UserID == item.UserID);
                        }
                    }


                    var banResult = query.ToList();

                    var paginateResult = new Paginate<Ban>
                    {
                        Items = banResult,
                        Index = index,
                        Count = banResult.Count,
                        From = index * size,
                        Size = size
                    };

                    return paginateResult;

                });


        public static void SetupAddAsync(Mock<IBanRepository> mockRepo, BanFakeData banFakeData) =>
            mockRepo.Setup(s => s.AddAsync(It.IsAny<Ban>()))
            .ReturnsAsync((Ban ban) =>
            {
                banFakeData.Data.Add(ban);
                return ban;
            });

        public static void SetupUpdateAsync(Mock<IBanRepository> mockRepo, BanFakeData banFakeData) =>
            mockRepo.Setup(s => s.UpdateAsync(It.IsAny<Ban>()))
            .ReturnsAsync((Ban ban) =>
            {
                Ban? result = banFakeData.Data.FirstOrDefault(x => x.Id!.Equals(ban.Id));
                if (result != null)
                    result = ban;
                return result;
            });

        public static void SetupDeleteAsync(Mock<IBanRepository> mockRepo, BanFakeData banFakeData) =>
            mockRepo.Setup(s => s.DeleteAsync(It.IsAny<Ban>(), It.IsAny<bool>()))
            .ReturnsAsync((Ban ban, bool permanent) =>
            {
                if (!permanent)
                {

                    ban.DeletedDate = DateTime.UtcNow;
                }
                else
                    banFakeData.Data.Remove(ban);
                return ban;
            });


        public static void Build(Mock<IBanRepository> mockRepo, BanFakeData banFakeData, SiteUserFakeData siteUserFakeData, ReportFakeData reportFakeData)
        {
            SetupGetListAsync(mockRepo, banFakeData, siteUserFakeData, reportFakeData);
            SetupAddAsync(mockRepo, banFakeData);
            SetupUpdateAsync(mockRepo, banFakeData);
            SetupDeleteAsync(mockRepo, banFakeData);
        }

        public static Mock<IBanRepository> GetRepository(BanFakeData banFakeData, SiteUserFakeData siteUserFakeData, ReportFakeData reportFakeData)
        {
            var mockRepo = new Mock<IBanRepository>();

            Build(mockRepo, banFakeData, siteUserFakeData, reportFakeData);

            return mockRepo;
        }


    }
}
