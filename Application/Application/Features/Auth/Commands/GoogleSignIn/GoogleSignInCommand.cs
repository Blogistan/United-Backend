using Application.Features.Auth.Commands.Login;
using Application.Services.Auth;
using Application.Services.Repositories;
using Google.Apis.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Auth.Commands.GoogleSignIn
{
    public class GoogleSignInCommand:IRequest<LoginResponse>
    {
        public string IdToken { get; set; }
        public string TokenLifeTime { get; set; }
        public string IpAdress { get; set; }

        public class GoogleSignInCommandHandler : IRequestHandler<GoogleSignInCommand, LoginResponse>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly IAuthService authService;
            private readonly IConfiguration configuration;
            public GoogleSignInCommandHandler(ISiteUserRepository siteUserRepository, IAuthService authService, IConfiguration configuration)
            {
                this.siteUserRepository = siteUserRepository;
                this.authService = authService;
                this.configuration = configuration;
            }
            public async Task<LoginResponse> Handle(GoogleSignInCommand request, CancellationToken cancellationToken)
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string> { configuration["ExternalLoginSettings:Google:Client_ID"] }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);


            }
        }

    }
}
