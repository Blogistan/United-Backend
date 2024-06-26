﻿using Core.Application.Responses;
using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Features.Auth.Commands.Refresh
{
    public class RefreshedResponse:IResponse
    {
        public AccessToken AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}
