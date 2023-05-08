using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.DeleteCategory;
using Application.Features.Categories.Commands.UpdateCategory;
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
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCategoryCommand deleteCategoryCommand)
        {
            DeleteCategoryResponse response = await Mediator.Send(deleteCategoryCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand updateCategoryCommand)
        {
            UpdateCategoryResponse response = await Mediator.Send(updateCategoryCommand);
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
