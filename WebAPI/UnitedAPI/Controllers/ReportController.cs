using Application.Features.Reports.Commands.CreateReport;
using Application.Features.Reports.Commands.DeleteReport;
using Application.Features.Reports.Commands.UpdateReport;
using Application.Features.Reports.Queries.GetListReport;
using Application.Features.Reports.Queries.GetListReportDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListReportQuery getListReportQuery = new() { PageRequest = pageRequest };
            GetListReportQueryResponse response = await Mediator.Send(getListReportQuery);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> GetListDynamic([FromBody] GetListReportDynamicQuery getListReportDynamicQuery)
        {
            GetListReportDynamicQueryResponse response = await Mediator.Send(getListReportDynamicQuery);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReportCommand createReportCommand)
        {
            createReportCommand.CreateUser = GetUserIdFromToken();
            CreateReportCommandResponse response = await Mediator.Send(createReportCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateReportCommand updateReportCommand)
        {
            UpdateReportCommandResponse response = await Mediator.Send(updateReportCommand);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteReportCommand deleteReportCommand)
        {
            DeleteReportCommandResponse response = await Mediator.Send(deleteReportCommand);
            return Ok(response);
        }
    }
}
