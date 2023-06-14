using Application.Features.Bookmarks.Queries.AddToBookmarks;
using Application.Features.Bookmarks.Queries.RemoveFromBookmarks;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookmarkController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> AddToBookmarks([FromBody] AddToBookmarksQuery addToBookmarksQuery)
        {
            var result = await Mediator.Send(addToBookmarksQuery);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromBookmarks([FromBody] RemoveFromBookmarkQuery removeFromBookmarkQuery)
        {
            var result = await Mediator.Send(removeFromBookmarkQuery);
            return Ok(result);
        }
    }
}
