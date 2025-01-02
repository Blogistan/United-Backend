using Application.Features.Bookmarks.Queries.AddToBookmarks;
using Application.Features.Bookmarks.Queries.GetListBookmarks;
using Application.Features.Bookmarks.Queries.RemoveFromBookmarks;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookmarkController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddToBookmarksQuery addToBookmarksQuery)
        {
            var result = await Mediator.Send(addToBookmarksQuery);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] RemoveFromBookmarkQuery removeFromBookmarkQuery)
        {
            var result = await Mediator.Send(removeFromBookmarkQuery);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetListBookmarksQuery getListBookmarksQuery)
        {
            var response = await Mediator.Send(getListBookmarksQuery);
            return Ok(response);
        }
    }
}
