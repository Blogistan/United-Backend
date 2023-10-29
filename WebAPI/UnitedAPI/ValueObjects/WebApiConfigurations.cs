namespace UnitedAPI.ValueObjects
{
    public class WebApiConfigurations
    {
        public string AuthVerifyEmailUrl { get; set; } = string.Empty;
        public string SecretKeyLabel { get; set; }= string.Empty;
        public string SecretKeyIssuer { get; set; } = string.Empty;
        public string PasswordResetUrl { get; set; } = string.Empty;
    }
}
