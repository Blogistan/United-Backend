using Microsoft.SemanticKernel.ChatCompletion;

namespace Application.Services.Assistant.Services
{
    public static class HistoryService
    {
        private static readonly Dictionary<string, ChatHistory> chatHistories = new();

        public static ChatHistory GetChatHistory(string connectionId)
        {
            ChatHistory chatHistory = null;
            if (chatHistories.TryGetValue(connectionId, out chatHistory))
                return chatHistory;
            else
            {
                chatHistory = new();
                chatHistories.Add(connectionId, chatHistory);
            }
            return chatHistory;
        }
    }
}
