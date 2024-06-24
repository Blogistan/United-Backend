using Application.Features.ReportTypes.Queries.GetListReportTypes;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.ReportTypes.Queries.GetListReportTypes.GetListReportTypeQuery;

namespace AuthTest.Features.ReportTypes.Queries.GetList
{
    public class GetListReportTypeTest : ReportTypeMockRepository, IClassFixture<Startup>
    {
        private readonly GetListReportTypeQuery _query;
        private readonly GetListReportTypeQueryHandler _handler;
        private readonly GetListReportTypeQueryValidator _validator;

        public GetListReportTypeTest(ReportTypeFakeData fakeData) : base(fakeData)
        {
            this._query = new GetListReportTypeQuery();
            this._validator = new GetListReportTypeQueryValidator();
            this._handler = new GetListReportTypeQueryHandler(MockRepository.Object, Mapper);
        }
        [Fact]
        public async Task ThrowExceptionIfIdIfEmpty()
        {

            TestValidationResult<GetListReportTypeQuery> testValidationResult = _validator.TestValidate(_query);

            testValidationResult.ShouldHaveValidationErrorFor(x => x.PageRequest);
        }
        [Fact]
        public async Task GetReportTypesSuccessfully()
        {
            _query.PageRequest = new Core.Application.Requests.PageRequest { Page = 0, PageSize = 30 };

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.NotNull(result.Items);


        }
    }
}
