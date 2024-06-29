using Application.Features.Blogs.Queries.DecreaseTriggerBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.DecreaseTriggerBlog.DecreaseTriggerBlogQuery;

namespace AuthTest.Features.Blogs.Queries.TriggerBlog
{
    public class DecreaseTriggerBlogQueryTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly DecreaseTriggerBlogQuery _query;
        private readonly DecreaseTriggerBlogQueryValidator _validator;
        private readonly DecreaseTriggerBlogQueryHandler _handler;
        public DecreaseTriggerBlogQueryTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new DecreaseTriggerBlogQuery();
            this._validator = new DecreaseTriggerBlogQueryValidator();
            this._handler = new DecreaseTriggerBlogQueryHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            TestValidationResult<DecreaseTriggerBlogQuery> testValidationResult = _validator.TestValidate(_query);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
        }
        [Fact]
        public async Task IncreaseSuprisedCountSuccessfully()
        {
            _query.BlogId = 1;

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.Equal(-1, result.ReactionTriggeredCount);
        }
        [Fact]
        public async Task ThrowExceptionIfBlogNotExists()
        {
            _query.BlogId = 54165;
            await Assert.ThrowsAsync<BusinessException>(async () =>
            {
                await _handler.Handle(_query, CancellationToken.None);
            });
        }
    }
}
