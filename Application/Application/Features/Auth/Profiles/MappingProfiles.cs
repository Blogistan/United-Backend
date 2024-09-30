using Application.Features.Auth.Commands.Revoke;
using Application.Features.Auth.Dtos;
using AutoMapper;
using Core.Application.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;

namespace Application.Features.Auth.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserForRegisterDto>().ReverseMap();

            CreateMap<AccessToken, AccessTokenDto>().ReverseMap();
            CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
            CreateMap<RevokedResponse, RefreshToken>().ReverseMap();

           
        }
    }
}
