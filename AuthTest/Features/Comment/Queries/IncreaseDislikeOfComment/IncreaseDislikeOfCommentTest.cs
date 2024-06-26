﻿using Application.Features.Comments.Profiles;
using Application.Features.Comments.Profiles;
using Application.Features.Comments.Queries.DecreaseDislikeOfCommentQuery;
using Application.Features.Comments.Queries.GetCommentResponsesQuery;
using Application.Features.Comments.Queries.IncreaseDislikeOfCommentQuery;
using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using AutoMapper;
using FluentValidation.TestHelper;
using static Application.Features.Comments.Queries.GetCommentResponsesQuery.GetCommentResponsesQuery;
using static Application.Features.Comments.Queries.IncreaseDislikeOfCommentQuery.IncreaseDislikeOfCommentQuery;

namespace AuthTest.Features.Comment.Queries.IncreaseDislikeOfComment
{
    public class IncreaseDislikeOfCommentTest : IClassFixture<Startup>
    {
        private readonly IncreaseDislikeOfCommentQuery _query;
        private readonly IncreaseDislikeOfCommentQueryValidator _validationRules;
        private readonly IncreaseDislikeOfCommentQueryHandler _handler;
        private CommentFakeData _commentFakeData;
        private SiteUserFakeData _siteUserFakeData;

        public IncreaseDislikeOfCommentTest(CommentFakeData commentFakeData, SiteUserFakeData siteUserFakeData)
        {

            ICommentRepository commentRepository = CommentMockRepository.GetRepository(commentFakeData, siteUserFakeData).Object;
            CommentBusinessRules commentBusinessRules = new CommentBusinessRules(commentRepository);

            MapperConfiguration mapperConfig = new(c =>
            {
                c.AddProfile<MappingProfiles>();
            });

            IMapper mapper = mapperConfig.CreateMapper();

            this._commentFakeData = commentFakeData;
            this._siteUserFakeData = siteUserFakeData;

            this._query = new IncreaseDislikeOfCommentQuery();
            this._handler = new IncreaseDislikeOfCommentQueryHandler(commentRepository);
            this._validationRules = new IncreaseDislikeOfCommentQueryValidator();
        }
        [Fact]
        public async Task ThrowExceptionIfCommentIsNull()
        {
            TestValidationResult<IncreaseDislikeOfCommentQuery> testValidationResult = _validationRules.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.CommentId);
        }
        [Fact]
        public async Task IncreaseDislikeSuccessfully()
        {
            _query.CommentId = 1;
            var result = await _handler.Handle(_query, CancellationToken.None);
            Assert.Equal(1, result.Dislikes);

        }
        [Fact]
        public async Task ThrowExceptinIfCommentIdIsEmpty()
        {
            TestValidationResult<IncreaseDislikeOfCommentQuery> testValidationResult = _validationRules.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.CommentId);
        }
    }
}
