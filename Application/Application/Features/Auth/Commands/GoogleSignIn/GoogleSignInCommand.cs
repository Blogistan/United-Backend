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

                return await CreateUserExternalAsync(user, payload.Email, payload.Name, payload.FamilyName, payload.Picture, request.IpAdress);

            }

            private async Task<LoginResponse> CreateUserExternalAsync(SiteUser user, string email, string name, string surname, string picture, string ipAdress)
            {
                bool result = user != null;

                AccessToken accessToken = new();
                RefreshToken refreshToken = new();

                LoginResponse loginResponse = new();

                if (user == null)
                {
                    SiteUser siteUser = new()
                    {
                        FirstName = name,
                        LastName = surname,
                        Email = email,
                        Status = true,
                        ProfileImageUrl = picture
                    };

                    var createdUser = await siteUserRepository.AddAsync(siteUser);
                    accessToken = await authService.CreateAccessToken(siteUser);
                    refreshToken = await authService.CreateRefreshToken(siteUser, ipAdress);

                    await authService.AddRefreshToken(refreshToken);

                    loginResponse.RefreshToken = refreshToken;
                    loginResponse.AccessToken = accessToken;
                }
                else
                {
                    accessToken = await authService.CreateAccessToken(user);
                    refreshToken = await authService.CreateRefreshToken(user, ipAdress);
                    await authService.AddRefreshToken(refreshToken);

                    loginResponse.RefreshToken = refreshToken;
                    loginResponse.AccessToken = accessToken;
                }

                return loginResponse;
            }
        }
    }
}
