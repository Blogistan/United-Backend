﻿using Application.Features.Reports.Commands.DeleteReport;
using Application.Features.ReportTypes.Commands.CreateReportType;
using Application.Features.ReportTypes.Commands.DeleteReportType;
using Application.Features.ReportTypes.Commands.UpdateReportType;
using Application.Features.ReportTypes.Queries.GetListReportTypes;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportTypeController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetListReportType([FromQuery] PageRequest pageRequest)
        {
            GetListReportTypeQuery getListReportTypeQuery = new() { PageRequest = pageRequest };
            GetListReportTypeQueryResponse response = await Mediator.Send(getListReportTypeQuery);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReportType([FromBody] CreateReportTypeCommand createReportTypeCommand)
        {
            CreateReportTypeCommandResponse response = await Mediator.Send(createReportTypeCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReportType([FromBody] UpdateReportTypeCommand updateReportTypeCommand)
        {
            UpdateReportTypeCommandResponse response = await Mediator.Send(updateReportTypeCommand);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteReportType([FromBody] DeleteReportTypeCommand deleteReportTypeCommand)
        {
            DeleteReportTypeCommandResponse response = await Mediator.Send(deleteReportTypeCommand);
            return Ok(response);
        }
    }
}
