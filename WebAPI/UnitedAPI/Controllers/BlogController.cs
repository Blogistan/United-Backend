using Application.Features.Blogs.Commands.CreateBlog;
using Application.Features.Blogs.Commands.DeleteBlog;
using Application.Features.Blogs.Commands.UpdateBlog;
using Application.Features.Blogs.Queries.GetListBlog;
using Application.Features.Blogs.Queries.GetListBlogDynamic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateBlogCommand createBlogCommand)
        {
            CreateBlogCommandResponse response = await Mediator.Send(createBlogCommand);

            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteBlogCommand deleteBlogCommand)
        {
            DeleteBlogCommandResponse response = await Mediator.Send(deleteBlogCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateBlogCommand updateBlogCommand)
        {
            UpdateBlogCommandResponse response = await Mediator.Send(updateBlogCommand);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListBlogQuery getListBlogQuery = new() { PageRequest = pageRequest };
            GetListBlogQueryResponse response = await Mediator.Send(getListBlogQuery);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetListDynamic([FromQuery] PageRequest pageRequest, [FromQuery] DynamicQuery dynamicQuery)
        {
            GetListBlogDynamicQuery getListBlogDynamicQuery = new() { PageRequest = pageRequest,DynamicQuery=dynamicQuery };
            GetListBlogDynamicQueryResponse response = await Mediator.Send(getListBlogDynamicQuery);
            return Ok(response);
        }
    }
}
