using Application.Features.Reports.Queries.GetListReportDynamic;
using AuthTest.Mocks.FakeDatas;
using AuthTest.Mocks.Repositories;
using FluentValidation.TestHelper;
using static Application.Features.Reports.Queries.GetListReportDynamic.GetListReportDynamicQuery;

namespace AuthTest.Features.Reports.Queries.GetListDynamic
{
    public class GetListDynamicReportTest : ReportMockRepository, IClassFixture<Startup>
    {
        private readonly GetListReportDynamicQuery _query;
        private readonly GetListReportDynamicQueryValidator _validator;
        private readonly GetListReportDynamicQueryHandler _handler;
        public GetListDynamicReportTest(ReportFakeData fakeData) : base(fakeData)
        {
            this._query = new GetListReportDynamicQuery();
            this._validator = new GetListReportDynamicQueryValidator();
            this._handler = new GetListReportDynamicQueryHandler(MockRepository.Object, Mapper);
        }
        [Fact]
        public async Task ThrowExceptionIfPageRequestIsEmpty()
        {
            this._query.DynamicQuery = new Core.Persistence.Dynamic.DynamicQuery();
            TestValidationResult<GetListReportDynamicQuery> testValidationResult = _validator.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.PageRequest);

        }
        [Fact]
        public async Task ThrowExceptionIfDynamicQueryIsEmpty()
        {
            this._query.PageRequest = new Core.Application.Requests.PageRequest { Page = 0, PageSize = 1 };
            TestValidationResult<GetListReportDynamicQuery> testValidationResult = _validator.TestValidate(_query);
            testValidationResult.ShouldHaveValidationErrorFor(x => x.DynamicQuery);

        }
    }
}
