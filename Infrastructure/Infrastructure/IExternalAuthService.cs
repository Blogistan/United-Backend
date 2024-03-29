﻿using Domain.Entities;
using Infrastructure.Dtos;
using Infrastructure.Dtos.Facebook;
using Infrastructure.Dtos.Github;
using Infrastructure.Dtos.Twitter;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Infrastructure
{
    public interface IExternalAuthService
    {
        Task<LoginResponseBase> CreateUserExternalAsync(SiteUser user, string email, string name, string? surname, string? picture, string ipAdress);
        Task<Payload> GoogleSignIn(string idToken);
        Task<FacebookUserInfoResponse> FacebookSignIn(string authToken);
        Task<OAuthResponse> TwitterSignIn(OAuthCredentials oAuthCredentials);
        Task<TwitterUserInfo> GetTwitterUserInfo(OAuthResponse oAuthResponse);
        Task<string> GithubSignIn(string code);
        Task<GithubUserInfo> GithubUserInfo(string bearerToken);
    }
}
