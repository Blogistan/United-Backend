using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.CreateRangeCategory;
using Application.Features.Categories.Commands.DeleteCategory;
using Application.Features.Categories.Commands.DeleteRangeCategory;
using Application.Features.Categories.Commands.UpdateCategory;
using Application.Features.Categories.Commands.UpdateRangeCategory;
using Application.Features.Categories.Dtos;
using Application.Features.Categories.Queries.GetListCategory;
using Application.Features.Categories.Queries.GetListCategoryByDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            CreateCategoryResponse response = await Mediator.Send(createCategoryCommand);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddRange([FromBody] CreateRangeCategoryCommand createRangeCategoryCommand)
        {
            CreateRangeCategoryResponse response = await Mediator.Send(createRangeCategoryCommand);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand deleteCategoryCommand)
        {
            DeleteCategoryResponse response = await Mediator.Send(deleteCategoryCommand);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRange([FromBody] DeleteRangeCategoryCommand deleteRangeCategoryCommand)
        {
            DeleteRangeCategoryResponse response = await Mediator.Send(deleteRangeCategoryCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand updateCategoryCommand)
        {
            UpdateCategoryResponse response = await Mediator.Send(updateCategoryCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRange([FromBody] UpdateRangeCategoryCommand updateRangeCategoryCommand)
        {
            UpdateRangeCategoryResponse response = await Mediator.Send(updateRangeCategoryCommand);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCategoryQuery getListCategoryQuery = new()
            {
                PageRequest = pageRequest
            };

            CategoryListDto categoryListDto = await Mediator.Send(getListCategoryQuery);

            return Ok(categoryListDto);
        }
        [HttpGet]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,[FromQuery] DynamicQuery dynamicQuery)
        {
            GetListCategoryQueryByDynamicQuery getListCategoryQueryByDynamicQuery = new()
            {
                PageRequest = pageRequest,
                Dynamic=dynamicQuery
            };

            CategoryListDto categoryListDto = await Mediator.Send(getListCategoryQueryByDynamicQuery);

            return Ok(categoryListDto);
        }
    }
}
