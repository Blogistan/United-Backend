using Application.Features.Comments.Commands.CreateComment;
using Application.Features.Comments.Commands.DeleteComment;
using Application.Features.Comments.Commands.UpdateComment;
using Application.Features.Comments.Queries.DecreaseLikeOfCommentQuery;
using Application.Features.Comments.Queries.IncreaseLikeOfCommentQuery;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand createCommentCommand)
        {
            CreateCommentCommandResponse response = await Mediator.Send(createCommentCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentCommand updateCommentCommand)
        {
            UpdateCommentResponse response = await Mediator.Send(updateCommentCommand);
            return Ok(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteComment([FromBody] DeleteCommentCommand deleteCommentCommand)
        {
            DeleteCommentCommandResponse response = await Mediator.Send(deleteCommentCommand);
            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> IncreaseLike([FromBody] IncreaseLikeOfCommentQuery increaseLikeOfCommentQuery)
        {
            IncreaseLikeOfCommentQueryResponse response = await Mediator.Send(increaseLikeOfCommentQuery);
            return Ok(response);
        }

        //To do : Dislike Query features
        [HttpPut]
        public async Task<IActionResult> DecreaseLike ([FromBody] DecreaseLikeOfCommentQuery decreaseLikeOfCommentQuery)
        {
            DecreaseLikeOfCommentQueryResponse response = await Mediator.Send(decreaseLikeOfCommentQuery);
            return Ok(response);
        }
    }
}
