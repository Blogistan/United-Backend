using Application.Features.Auth.Commands.Revoke;
using Application.Features.Auth.Dtos;
using AutoMapper;
using Core.Application.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using Domain.Entities;

namespace Application.Features.Auth.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserForRegisterDto>().ReverseMap();
            CreateMap<SiteUser, UserForRegisterDto>().ForMember(opt => opt.FirstName, src => src.MapFrom(x => x.User.FirstName))
                .ForMember(opt => opt.LastName, src => src.MapFrom(x => x.User.LastName))
                .ForMember(opt=>opt.Email,src=>src.MapFrom(x=>x.User.Email))
                .ReverseMap();

            CreateMap<AccessToken, AccessTokenDto>().ReverseMap();
            CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();
            CreateMap<RevokedResponse, RefreshToken>().ReverseMap();

           
        }
    }
}
