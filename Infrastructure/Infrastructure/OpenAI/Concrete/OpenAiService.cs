using Domain.Entities;
using Infrastructure.OpenAI.Abstract;
using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using System.ClientModel;
using System.Text.Json;

namespace Infrastructure.OpenAI.Concrete
{
    public class OpenAiService : IOpenAiService
    {
        private readonly ChatClient chatClient;
        private readonly IConfiguration configuration;
        public OpenAiService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.chatClient = new(model: "gpt-4o", apiKey: this.configuration.GetSection("OpenAi:ApiKey").Get<string>());
        }
        public async Task<string> GenerateResponse(string prompt)
        {
            string jsonPayload = $$"""
        {
   "model": "gpt-4o",
   "messages": [
       {
           "role": "system",
           "content": "You are an AI assistant that generates structured blog articles."
       },
       {
           "role": "user",
           "content": "{{prompt}}"
       }
   ]
}
""";
            BinaryData input = BinaryData.FromString(jsonPayload);
            using BinaryContent content = BinaryContent.Create(input);
            ClientResult result = await chatClient.CompleteChatAsync(content);

            BinaryData output = result.GetRawResponse().Content;

            using JsonDocument outputAsJson = JsonDocument.Parse(output.ToString());
            string message = outputAsJson.RootElement
                .GetProperty("choices"u8)[0]
                .GetProperty("message"u8)
                .GetProperty("content"u8)
                .GetString();

            return message;
        }
    }
}
