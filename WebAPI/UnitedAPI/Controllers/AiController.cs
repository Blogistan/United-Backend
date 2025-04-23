using Application.Features.Ai.Queries.GeneratePromptResponse;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AiController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> GeneratePrompt([FromBody] GeneratePromptResponseQuery generatePromptResponseQuery)
        {
            GeneratePromptResponseQueryResponse response = await Mediator.Send(generatePromptResponseQuery);
            return Ok(response);
        }
    }
}
