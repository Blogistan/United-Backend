namespace UnitedAPI.ValueObjects
{
    public class WebApiConfigurations
    {
        public string AuthVerifyEmailUrl { get; set; }
        public string SecretKeyLabel { get; set; }
        public string SecretKeyIssuer { get; set; }
        public string PasswordResetUrl { get; set; }
    }
}
