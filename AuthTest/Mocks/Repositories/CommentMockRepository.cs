using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace AuthTest.Mocks.Repositories
{
    public static class CommentMockRepository
    {
        public static void SetupGetListAsync(Mock<ICommentRepository> mockRepo, CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData) =>
            mockRepo.Setup(s => s.GetListAsync(
                It.IsAny<Expression<Func<Comment, bool>>>(),
                It.IsAny<Func<IQueryable<Comment>, IOrderedQueryable<Comment>>>(),
                It.IsAny<Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>>>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync((
                Expression<Func<Comment, bool>>? predicate,
                Func<IQueryable<Comment>, IOrderedQueryable<Comment>>? orderBy,
                Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>>? include,
                int index,
                int size,
                bool withDeleted,
                bool enableTracking,
                CancellationToken cancellationToken
                ) =>
                {
                    var query = commentFakeData.Data.AsQueryable();

                    if (predicate != null)
                        query.Where(predicate.Compile());

                    if (include != null)
                    {
                        foreach (var item in commentFakeData.Data)
                        {
                            item.User = siteUserFakeData.Data.FirstOrDefault(s => s.Id == item.UserId);
                        }


                    }


                    var comments = query.ToList();

                    var paginateResult = new Paginate<Comment>
                    {
                        Items = comments,
                        Index = index,
                        Count = comments.Count,
                        From = index * size,
                        Size = size
                    };

                    return paginateResult;
                });



        public static void SetupGetAsync(Mock<ICommentRepository> mockRepo, CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData) =>
        mockRepo.Setup(s => s.GetAsync(
                It.IsAny<Expression<Func<Comment, bool>>>(),
                It.IsAny<Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>>>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()
                )).ReturnsAsync((
                Expression<Func<Comment, bool>> expression,
                Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>> include,
                bool withDeleted,
                    bool enableTracking,
                    CancellationToken cancellationToken
                ) =>
                {
                    var query = commentFakeData.Data.AsQueryable();

                    SiteUser siteUser = new SiteUser();

                    if (include != null)
                    {
                        query = include(query);
                    }

                    var result = query.FirstOrDefault(expression.Compile());

                    if (result != null)
                    {
                        result.User = siteUserFakeData.Data.FirstOrDefault(x => x.Id == result.UserId);
                        result.CommentResponses = commentFakeData.Data.Where(x => x.CommentId == result.Id).ToList();
                    }


                    return result;

                });

        public static void SetupAddAsync(Mock<ICommentRepository> mockRepo, CommentFakeData commentFakeData) =>
            mockRepo.Setup(s => s.AddAsync(It.IsAny<Comment>()))
            .ReturnsAsync((Comment comment) =>
            {
                commentFakeData.Data.Add(comment);
                return comment;
            }
            );

        public static void SetupUpdateAsync(Mock<ICommentRepository> mockRepo, CommentFakeData commentFakeData) =>
            mockRepo.Setup(s => s.UpdateAsync(It.IsAny<Comment>()))
            .ReturnsAsync((Comment comment) =>
            {
                Comment? result = commentFakeData.Data.FirstOrDefault(x => x.Id!.Equals(comment.Id));
                if (result != null)
                    result = comment;

                return result;
            });

        public static void SetupDeleteAsync(Mock<ICommentRepository> mockRepo, CommentFakeData commentFakeData) =>
            mockRepo.Setup(s => s.DeleteAsync(It.IsAny<Comment>(), It.IsAny<bool>()))
            .ReturnsAsync((Comment comment, bool permanent) =>
            {
                if (!permanent)
                    comment.DeletedDate = DateTime.UtcNow;
                else
                    commentFakeData.Data.Remove(comment);

                return comment;
            });


        public static void Build(Mock<ICommentRepository> mockRepo, CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData)
        {
            SetupGetListAsync(mockRepo, commentFakeData, siteUserFakeData);
            SetupAddAsync(mockRepo, commentFakeData);
            SetupUpdateAsync(mockRepo, commentFakeData);
            SetupDeleteAsync(mockRepo, commentFakeData);
            SetupGetAsync(mockRepo, commentFakeData, siteUserFakeData);
        }
        public static Mock<ICommentRepository> GetRepository(CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData)
        {
            var mockRepo = new Mock<ICommentRepository>();

            Build(mockRepo, commentFakeData, siteUserFakeData);

            return mockRepo;
        }

    }
}
