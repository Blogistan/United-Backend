﻿using Application.Features.Auth.Commands.Login;
using Application.Services.Auth;
using Application.Services.Repositories;
using Infrastructure.Dtos.Twitter;
using MediatR;

namespace Application.Features.Auth.Commands.TwitterSignIn
{
    public class TwitterSignInCommand : IRequest<LoginResponse>
    {
        public string AccessToken { get; set; }
        public string TokenSecret { get; set; }
        public string IpAddress { get; set; }
        public Dictionary<string, string> Cookies { get; set; }
        public class TwitterSignInCommandHandler : IRequestHandler<TwitterSignInCommand, LoginResponse>
        {
            private IAuthService authService;
            private IUserRepository userRepository;

            public TwitterSignInCommandHandler(IUserRepository userRepository, IAuthService authService)
            {
                this.userRepository = userRepository;
                this.authService = authService;
            }

            public async Task<LoginResponse> Handle(TwitterSignInCommand request, CancellationToken cancellationToken)
            {
                var info = await authService.GetTwitterUserInfo(new OAuthResponse { Oauth_token = request.AccessToken, Oauth_token_secret = request.TokenSecret, Cookies = request.Cookies });
                var user = await userRepository.GetAsync(x => x.Email == info.email && x.IsActive == true);

                var result = await authService.CreateUserExternalAsync(user, info.email, info.screen_name, "", "", request.IpAddress,Core.Security.Enums.LoginProviderType.Twitter,info.id_str);

                return new LoginResponse
                {
                    AccessToken = result.AccessToken,
                    RefreshToken = result.RefreshToken
                };
            }
        }
    }
}
