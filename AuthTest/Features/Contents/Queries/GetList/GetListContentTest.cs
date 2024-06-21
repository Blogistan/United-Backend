using Application.Features.Contents.Profiles;
using Application.Features.Contents.Queries.GetListContent;
using Application.Features.Contents.Rules;
using Application.Services.Repositories;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using AutoMapper;
using FluentValidation.TestHelper;
using static Application.Features.Contents.Queries.GetListContent.GetListContentQuery;

namespace AuthTest.Features.Contents.Queries.GetList
{
    public class GetListContentTest : IClassFixture<Startup>
    {
        private readonly ContentFakeData contentFakeData;
        private readonly GetListContentQuery _query;
        private readonly GetListContentQueryValidator _validator;
        private readonly GetListContentQueryHandler _handler;

        public GetListContentTest(ContentFakeData contentFakeData)
        {
            this.contentFakeData = contentFakeData;

            IContentRepository contentRepository = ContentMockRepository.GetContentMockRepository(contentFakeData).Object;

            ContentBusinessRules contentBusinessRules = new ContentBusinessRules(contentRepository);

            MapperConfiguration mapperConfig = new(c =>
            {
                c.AddProfile<MappingProfiles>();
            });

            IMapper mapper = mapperConfig.CreateMapper();

            this._handler = new GetListContentQueryHandler(contentRepository, mapper);
            this._validator = new GetListContentQueryValidator();
            this._query = new GetListContentQuery();
        }
        [Fact]
        public async Task ContentContentPragraphShouldNotEmpty()
        {
            TestValidationResult<GetListContentQuery> testValidationResult = _validator.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.PageRequest);
        }
        [Fact]
        public async Task ShouldUpdateSuccessfully()
        {
            _query.PageRequest = new Core.Application.Requests.PageRequest { Page = 0, PageSize = 10 };
            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.NotNull(result);
        }

    }
}
