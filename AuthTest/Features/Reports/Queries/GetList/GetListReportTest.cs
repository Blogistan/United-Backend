using Application.Features.Reports.Queries.GetListReport;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.Reports.Queries.GetListReport.GetListReportQuery;

namespace AuthTest.Features.Reports.Queries.GetList
{
    public class GetListReportTest : ReportMockRepository, IClassFixture<Startup>
    {
        private readonly GetListReportQuery _query;
        private readonly GetListReportQueryValidator _validator;
        private readonly GetListReportQueryHandler _handler;
        public GetListReportTest(ReportFakeData fakeData) : base(fakeData)
        {
            this._query = new GetListReportQuery();
            this._validator = new GetListReportQueryValidator();
            this._handler = new GetListReportQueryHandler(MockRepository.Object, Mapper);
        }
        [Fact]
        public async Task ThrowExceptionIfPageRequestIsEmpty()
        {
            TestValidationResult<GetListReportQuery> testValidationResult = _validator.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.PageRequest);

        }
        [Fact]
        public async Task GetReportsSuccessfully()
        {
            this._query.PageRequest = new Core.Application.Requests.PageRequest { Page = 0, PageSize = 10 };

            var result = await _handler.Handle(_query, CancellationToken.None);

            Assert.NotNull(result.Items);
        }
    }
}
