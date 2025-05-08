namespace Infrastructure.HuggingFace.Abstract
{
    public interface IHuggingFaceService
    {
        Task<byte[]> TextToSpeechAsync(string text);
        Task<string> SpeechToTextAsync(byte[] audioBytes);
    }
}
