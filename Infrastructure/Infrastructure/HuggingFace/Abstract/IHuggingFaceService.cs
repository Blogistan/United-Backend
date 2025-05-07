namespace Infrastructure.HuggingFace.Abstract
{
    public interface IHuggingFaceService
    {
        Task<byte[]> TextToSpeechAsync(string text, string model);
        Task<string> SpeechToTextAsync(byte[] audioBytes, string model);
    }
}
