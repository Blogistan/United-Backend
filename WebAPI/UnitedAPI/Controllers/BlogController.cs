using Application.Features.Blogs.Commands.CreateBlog;
using Application.Features.Blogs.Commands.DeleteBlog;
using Application.Features.Blogs.Commands.UpdateBlog;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogController:BaseController
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
    }
}
