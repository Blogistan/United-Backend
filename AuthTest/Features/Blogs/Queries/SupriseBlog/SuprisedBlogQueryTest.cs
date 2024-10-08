﻿using Application.Features.Blogs.Queries.SuprisedBlog;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using FluentValidation.TestHelper;
using static Application.Features.Blogs.Queries.SuprisedBlog.SurprisedBlogQuery;

namespace AuthTest.Features.Blogs.Queries.SupriseBlog
{
    public class SuprisedBlogQueryTest : BlogMockRepository, IClassFixture<Startup>
    {
        private readonly SurprisedBlogQuery _query;
        private readonly SurprisedBlogQueryValidator _validator;
        private readonly SuprisedBlogQueryHandler _handler;
        public SuprisedBlogQueryTest(BlogFakeData fakeData) : base(fakeData)
        {
            this._query = new SurprisedBlogQuery();
            this._validator = new SurprisedBlogQueryValidator();
            this._handler = new SuprisedBlogQueryHandler(MockRepository.Object, Mapper, BusinessRules);
        }
        [Fact]
        public async Task ThrowExceptionIfTitleIsEmpty()
        {
            TestValidationResult<SurprisedBlogQuery> testValidationResult = _validator.TestValidate(_query);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.BlogId);
        }
        [Fact]
        public async Task IncreaseSuprisedCountSuccessfully()
        {
            _query.BlogId = 1;

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.Equal(1, result.ReactionSuprisedCount);
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
