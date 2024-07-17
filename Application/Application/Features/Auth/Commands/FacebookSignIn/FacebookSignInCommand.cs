using Application.Services.Auth;
using Application.Services.Repositories;
using Infrastructure.Dtos.Facebook;
using MediatR;

namespace Application.Features.Auth.Commands.FacebookSignIn
{
    public class FacebookSignInCommand : IRequest<FacebookLoginResponse>
    {
        public string Token { get; set; }
        public string IpAddress { get; set; }

        public class FacebookSignInCommandHandler : IRequestHandler<FacebookSignInCommand, FacebookLoginResponse>
        {
            private IAuthService authService;
            private ISiteUserRepository siteUserRepository;
            public FacebookSignInCommandHandler(IAuthService authService, ISiteUserRepository siteUserRepository)
            {
                this.authService = authService;
                this.siteUserRepository = siteUserRepository;
            }

            public async Task<FacebookLoginResponse> Handle(FacebookSignInCommand request, CancellationToken cancellationToken)
            {
                var info = await authService.FacebookSignIn(request.Token);

                var user = await siteUserRepository.GetAsync(x => x.Email == info.Email &&  x.IsActive==true);

                var result = await authService.CreateUserExternalAsync(user, info.Email, info.Name, "", "", request.IpAddress,Core.Security.Enums.LoginProviderType.Facebook,info.Id);

                return new FacebookLoginResponse()
                {
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken
                };
            }
        }
    }
}
