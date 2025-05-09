using Application.Features.Ai.Queries.GeneratePromptResponse;
using Application.Features.Ai.Queries.STT;
using MediatR;
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
            await Mediator.Send(generatePromptResponseQuery);
            return Ok(new { Message = "Prompt received." });
        }
        [HttpPost]
        public async Task<IActionResult> SpeechToText(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Didn't send sound file.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var audioBytes = memoryStream.ToArray();

            var query = new SpeechToTextQuery { AudioBytes = audioBytes };
            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}
