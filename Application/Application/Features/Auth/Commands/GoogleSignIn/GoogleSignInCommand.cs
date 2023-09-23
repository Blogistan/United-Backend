using Application.Features.Auth.Commands.Login;
using Application.Services.Auth;
using Application.Services.Repositories;
using Google.Apis.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Auth.Commands.GoogleSignIn
{
    public class GoogleSignInCommand : IRequest<LoginResponse>
    {
        public string IdToken { get; set; }
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
                var payload = await authService.GoogleSignIn(request.IdToken);
                var user = await siteUserRepository.GetAsync(x => x.Email == payload.Email);
                var result = await authService.CreateUserExternalAsync(user, payload.Email, payload.Name, payload.FamilyName, payload.Picture, request.IpAdress);

                return new LoginResponse
                {
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken
                };

            }


        }
    }
}
