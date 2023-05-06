using Application.Features.Auth.Commands.Revoke;
using AutoMapper;
using Core.Application.Dtos;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.Auth.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SiteUser, UserForRegisterDto>().ReverseMap();

            CreateMap<RevokedResponse, RefreshToken>().ReverseMap();
        }
    }
}
