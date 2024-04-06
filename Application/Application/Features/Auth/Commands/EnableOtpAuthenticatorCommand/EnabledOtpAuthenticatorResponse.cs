using Core.Application.Responses;

namespace Application.Features.Auth.Commands.EnableOtpAuthenticatorCommand
{
    public class EnabledOtpAuthenticatorResponse : IResponse
    {
        public string SecretKey { get; set; } = string.Empty;
        public string SecketKeyUrl { get; set; } = string.Empty;

        public EnabledOtpAuthenticatorResponse()
        {

        }
        public EnabledOtpAuthenticatorResponse(string SecretKey, string SecretKeyUrl)
        {
            this.SecketKeyUrl = SecketKeyUrl;
            this.SecretKey = SecretKey;
        }
    }
}

