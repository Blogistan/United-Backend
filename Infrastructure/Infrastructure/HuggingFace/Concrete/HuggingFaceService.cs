using Infrastructure.Constants;
using Infrastructure.HuggingFace.Abstract;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Infrastructure.HuggingFace.Concrete
{
    public class HuggingFaceService : IHuggingFaceService
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey;

        public HuggingFaceService(IConfiguration configuration, HttpClient httpClient)
        {
            this.httpClient = httpClient;

            var baseUrl = ExternalAPIUrls.HuggingFaceApiInterface ?? throw new NullReferenceException("BaseUrl not found.");
            apiKey = configuration.GetValue<string>("HuggingFace:ApiKey") ?? throw new NullReferenceException("ApiKey not found.");

            this.httpClient.BaseAddress = new Uri(baseUrl);
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> SpeechToTextAsync(byte[] audioBytes, string model)
        {
            using var byteContent = new ByteArrayContent(audioBytes);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");

            var response = await httpClient.PostAsync($"/models/{model}", byteContent);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"STT API Error: {error}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent);
            return result != null && result.TryGetValue("text", out var text)
                ? text
                : throw new ApplicationException("Text conversion failed.");
        }

        public async Task<byte[]> TextToSpeechAsync(string text, string model)
        {
            var requestData = new { inputs = text };
            var content = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await httpClient.PostAsync($"/models/{model}", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"TTS API Error: {error}");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
