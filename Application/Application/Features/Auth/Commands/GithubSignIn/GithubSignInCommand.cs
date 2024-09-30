using Application.Features.Auth.Commands.Login;
using Application.Services.Auth;
using Application.Services.Repositories;
using MediatR;

namespace Application.Features.Auth.Commands.GithubSignIn
{
    public class GithubSignInCommand:IRequest<LoginResponse>
    {
        public string Token { get; set; }
        public string IpAddress { get; set; }


        public class GithubSignInCommandHandler:IRequestHandler<GithubSignInCommand,LoginResponse>
        {
            private IAuthService authService;
            private IUserRepository userRepository;
            public GithubSignInCommandHandler(IAuthService authService, IUserRepository userRepository)
            {
                this.authService = authService;
                this.userRepository = userRepository;
            }

            public async Task<LoginResponse> Handle(GithubSignInCommand request, CancellationToken cancellationToken)
            {
                var info = await authService.GithubUserInfo(request.Token);
                var user = await userRepository.GetAsync(x => x.Email == info.email && x.IsActive == true);

                var result = await authService.CreateUserExternalAsync(user, info.email, info.name, "", info.avatar_url, request.IpAddress,Core.Security.Enums.LoginProviderType.Github,info.node_id);

                return new LoginResponse
                {
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken
                };
            }
        }
    }
}
