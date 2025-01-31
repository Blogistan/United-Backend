using Application.Features.Auth.Commands.Login;
using Application.Services.Auth;
using Application.Services.Repositories;
using MediatR;

namespace Application.Features.Auth.Commands.GoogleSignIn
{
    public class GoogleSignInCommand : IRequest<LoginResponse>
    {
        public string Code { get; set; }
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
                var googleTokenResponse = await authService.GetGoogleToken(request.Code);
                var payload = await authService.GoogleSignIn(googleTokenResponse.IdToken);
                var user = await userRepository.GetAsync(x => x.Email == payload.Email);
                var result = await authService.CreateUserExternalAsync(user, payload.Email, payload.Name, payload.FamilyName, payload.Picture, request.IpAdress,Core.Security.Enums.LoginProviderType.Google, googleTokenResponse.AccessToken);
                return new LoginResponse
                {
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken
                };

            }


        }
    }
}
