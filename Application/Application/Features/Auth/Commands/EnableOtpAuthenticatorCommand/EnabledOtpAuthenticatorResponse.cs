namespace Application.Features.Auth.Commands.EnableOtpAuthenticatorCommand
{
    public class EnabledOtpAuthenticatorResponse
    {
        public string SecretKey { get; set; } = string.Empty;
        public string SecketKeyUrl { get; set; }= string.Empty;
    }
}

