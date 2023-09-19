using Application.Features.Auth.Commands.Login;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;
using Google.Apis.Auth;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Auth.Commands.GoogleSignIn
{
    public class GoogleSignInCommand : IRequest<LoginResponse>
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

                var user = await siteUserRepository.GetAsync(x => x.Email == payload.Email);





            }

            private async Task<LoginResponse> CreateUserExternalAsync(SiteUser user, string email, string name, string surname)
            {
                
                if (user == null)
                {
                    user = await siteUserRepository.GetAsync(x => x.Email == email);
                    if (user == null)
                    {
                        user = new()
                        {
                            Email = email,
                            FirstName = name,
                            LastName = surname,
                            Status = true,
                        };

                        var createdUser = await siteUserRepository.AddAsync(user);
                      
                    }
                }

                //var  userToBeLogin=user!=null?user:created

                //AccessToken accessToken = await authService.CreateAccessToken(());

                //await authService.DeleteOldActiveRefreshTokens(siteUser);

                //RefreshToken refreshToken = await authService.CreateRefreshToken(siteUser, request.IpAddress);

                //await authService.AddRefreshToken(refreshToken);

            }
        }
    }
}
