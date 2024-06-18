using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace AuthTest.Mocks.Repositories
{
    public static class ContentMockRepository
    {


        public static void SetupGetListAsync(Mock<IContentRepository> mockRepo, ContentFakeData contentFakeData) =>
            mockRepo.Setup(s => s.GetListAsync(
                It.IsAny<Expression<Func<Content, bool>>>(),
                It.IsAny<Func<IQueryable<Content>, IOrderedQueryable<Content>>>(),
                It.IsAny<Func<IQueryable<Content>, IIncludableQueryable<Content, object>>>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync((
                Expression<Func<Content, bool>>? predicate,
                Func<IQueryable<Content>, IOrderedQueryable<Comment>>? orderBy,
                Func<IQueryable<Content>, IIncludableQueryable<Comment, object>>? include,
                int index,
                int size,
                bool withDeleted,
                bool enableTracking,
                CancellationToken cancellationToken
                ) =>
                {
                    var query = contentFakeData.Data.AsQueryable();

                    if (!withDeleted)
                        query = query.Where(e => !e.DeletedDate.HasValue);

                    if (predicate != null)
                        query = query.Where(predicate);

                    IList<Content> list = query.ToList();

                    Paginate<Content> paginateList = new() { Items = list };
                    return paginateList;

                });


        public static void SetupGetAsync(Mock<IContentRepository> mockRepo, ContentFakeData contentFakeData)
        {
            mockRepo.Setup(s => s.GetAsync(
                It.IsAny<Expression<Func<Content, bool>>>(),
                It.IsAny<Func<IQueryable<Content>, IIncludableQueryable<Content, object>>>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()
                ))
            .ReturnsAsync((
                Expression<Func<Content, bool>>? expression,
                Func<IQueryable<Content>, IIncludableQueryable<Comment, object>>? include,
                bool withDeleted,
                bool enableTracking,
                CancellationToken cancellationToken
                ) =>
            {
                Content content = new Content();
                var query = contentFakeData.Data.AsQueryable();

                if (!withDeleted)
                    query = query.Where(e => !e.DeletedDate.HasValue);

                if (expression != null)
                    content = query.FirstOrDefault(expression);


                return content;

            });
        }

        public static void SetupAddAsync(Mock<IContentRepository> mockRepo, ContentFakeData contentFakeData) =>
            mockRepo.Setup(s => s.AddAsync(It.IsAny<Content>()))
            .ReturnsAsync((Content content) =>
            {
                contentFakeData.Data.Add(content);
                return content;
            });
        public static void SetupUpdateAsync(Mock<IContentRepository> mockRepo, ContentFakeData contentFakeData) =>
            mockRepo.Setup(s => s.UpdateAsync(It.IsAny<Content>()))
            .ReturnsAsync((Content content) =>
            {
                Content result = contentFakeData.Data.FirstOrDefault(x => x.Id!.Equals(content.Id));
                if (result != null)
                    result = content;
                return result;
            });

        public static void SetupDeleteAsync(Mock<IContentRepository> mockRepo, ContentFakeData contentFakeData) =>
        mockRepo
            .Setup(r => r.DeleteAsync(It.IsAny<Content>(), It.IsAny<bool>()))
            .ReturnsAsync(
                (Content entity, bool permanent) =>
                {
                    if (!permanent)
                        entity.DeletedDate = DateTime.UtcNow;
                    else
                        contentFakeData.Data.Remove(entity);
                    return entity;
                }
            );

        public static void GetContentMockRepository(Mock<IContentRepository> mockRepo, ContentFakeData contentFakeData)
        {
            SetupAddAsync(mockRepo,contentFakeData);
            SetupDeleteAsync(mockRepo, contentFakeData);
            SetupGetAsync(mockRepo, contentFakeData);
            SetupGetListAsync(mockRepo, contentFakeData);
            SetupUpdateAsync(mockRepo, contentFakeData);
        }
    }
}
