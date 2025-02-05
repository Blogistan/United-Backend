namespace Infrastructure.OpenAI.Abstract
{
    public interface IOpenAiService
    {
        Task<string> GenerateResponse(string prompt);
    }
}
