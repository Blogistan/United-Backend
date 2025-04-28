using Application.Services.Assistant.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

namespace Application.Services.Assistant.Services
{
    public class AiService(IHubContext<AiHub> hubContext, IChatCompletionService chatCompletionService, Kernel kernel)
    {
        public async Task GetMessageStreamAsync(string promt, string connectionId, CancellationToken? cancellationToken = default!)
        {
            PromptExecutionSettings promptExecutionSettings = new()
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };

            var history = HistoryService.GetChatHistory(connectionId);
            history.AddSystemMessage("You are a creative and helpful blog writing assistant. You produce sample content on blog topics provided by the user, comment on posts, do style analysis and suggest SEO tags.\r\n\r\nYour content should be original, simple and impressive. You can write humorously when necessary and seriously when necessary.");
            history.AddUserMessage(promt);
            string responseContent = "";

            await foreach (var response in chatCompletionService.GetStreamingChatMessageContentsAsync(history, promptExecutionSettings, kernel))
            {
                cancellationToken?.ThrowIfCancellationRequested();
                await hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", response.ToString());
                responseContent += response.ToString();
            }
            history.AddAssistantMessage(responseContent);
            history.AddAssistantMessage("[END]");

        }
    }
}
