using AutoMapper;
using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.Auth.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<SiteUser, UserForRegisterDto>();
        }
    }
}
