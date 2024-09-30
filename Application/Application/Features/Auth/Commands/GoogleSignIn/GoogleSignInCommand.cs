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
            private readonly IUserRepository userRepository;
            private readonly IAuthService authService;

            public GoogleSignInCommandHandler(IUserRepository userRepository, IAuthService authService)
            {
                this.userRepository = userRepository;
                this.authService = authService;

            }

            public async Task<LoginResponse> Handle(GoogleSignInCommand request, CancellationToken cancellationToken)
            {
                var payload = await authService.GoogleSignIn(request.IdToken);
                var user = await userRepository.GetAsync(x => x.Email == payload.Email && x.IsActive == true);
                var result = await authService.CreateUserExternalAsync(user, payload.Email, payload.Name, payload.FamilyName, payload.Picture, request.IpAdress,Core.Security.Enums.LoginProviderType.Google,payload.JwtId);
                return new LoginResponse
                {
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken
                };

            }


        }
    }
}
