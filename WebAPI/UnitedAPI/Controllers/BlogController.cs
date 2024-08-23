using Application.Features.Blogs.Commands.CreateBlog;
using Application.Features.Blogs.Commands.DeleteBlog;
using Application.Features.Blogs.Commands.UpdateBlog;
using Application.Features.Blogs.Dtos;
using Application.Features.Blogs.Queries.BlogDetailById;
using Application.Features.Blogs.Queries.DecreaseLovelyBlog;
using Application.Features.Blogs.Queries.DecreaseSadBlog;
using Application.Features.Blogs.Queries.DecreaseSuprisedBlog;
using Application.Features.Blogs.Queries.DecreaseTriggerBlog;
using Application.Features.Blogs.Queries.GetListBlog;
using Application.Features.Blogs.Queries.GetListBlogDynamic;
using Application.Features.Blogs.Queries.KEKWBlog;
using Application.Features.Blogs.Queries.LikeBlog;
using Application.Features.Blogs.Queries.LovelyBlog;
using Application.Features.Blogs.Queries.Reports.MostReaded;
using Application.Features.Blogs.Queries.Reports.MostShared;
using Application.Features.Blogs.Queries.SadBlog;
using Application.Features.Blogs.Queries.SuprisedBlog;
using Application.Features.Blogs.Queries.TriggerBlog;
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
        [HttpPost]
        public async Task<IActionResult> GetListDynamic([FromBody] GetListBlogDynamicQuery getListBlogDynamicQuery)
        {
            GetListBlogDynamicQueryResponse response = await Mediator.Send(getListBlogDynamicQuery);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> BlogKekw([FromBody] KekwBlogQuery kekwBlogQuery)
        {
            BlogListViewDto response = await Mediator.Send(kekwBlogQuery);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> BlogLovely([FromBody] LovelyBlogQuery lovelyBlogQuery)
        {
            BlogListViewDto response = await Mediator.Send(lovelyBlogQuery);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> BlogRead([FromBody] ReadBlogQuery readBlogQuery)
        {
            BlogListViewDto response = await Mediator.Send(readBlogQuery);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> BlogSad([FromBody] SadBlogQuery sadBlogQuery)
        {
            BlogListViewDto response = await Mediator.Send(sadBlogQuery);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> BlogSuprise([FromBody] SuprisedBlogQuery suprisedBlogQuery)
        {
            BlogListViewDto response = await Mediator.Send(suprisedBlogQuery);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> BlogTrigger([FromBody] TriggerBlogQuery triggerBlogQuery)
        {
            BlogListViewDto response = await Mediator.Send(triggerBlogQuery);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> BlogDetail([FromQuery] BlogDetailByIdQuery blogDetailByIdQuery)
        {
            BlogDetailDto response = await Mediator.Send(blogDetailByIdQuery);
            return Ok(response);

        }
        [HttpGet]
        public async Task<IActionResult> MostReadedBlogs([FromQuery] MostReadedBlogQuery mostReadedBlogQuery)
        {
            MostReadedBlogQueryResponse response = await Mediator.Send(mostReadedBlogQuery);
            return Ok(response);

        }
        [HttpGet]
        public async Task<IActionResult> MostSharedBlogs([FromQuery] MostSharedBlogQuery mostSharedBlogQuery)
        {
            MostSharedBlogQueryResponse response = await Mediator.Send(mostSharedBlogQuery);
            return Ok(response);

        }
        [HttpPut]
        public async Task<IActionResult> UnBlogTrigger([FromBody] DecreaseTriggerBlogQuery decreaseTriggerBlogQuery)
        {
            BlogListViewDto response = await Mediator.Send(decreaseTriggerBlogQuery);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UnBlogSad([FromBody] DecreaseSadBlogQuery decreaseSadBlogQuery)
        {
            BlogListViewDto response = await Mediator.Send(decreaseSadBlogQuery);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UnBlogLovely([FromBody] DecreaseLovelyBLogQuery decreaseLovelyBLogQuery)
        {
            BlogListViewDto response = await Mediator.Send(decreaseLovelyBLogQuery);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UnBlogSuprise([FromBody] DecreaseSuprisedBlogQuery suprisedBlogQuery)
        {
            BlogListViewDto response = await Mediator.Send(suprisedBlogQuery);
            return Ok(response);
        }
    }
}
