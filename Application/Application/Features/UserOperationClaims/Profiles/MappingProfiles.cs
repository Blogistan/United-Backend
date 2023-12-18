using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.DeleteUserOperationClaim;
using Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaimCommand;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Queries.GetListUsersOperationClaims;
using Application.Features.UserOperationClaims.Queries.GetListUsesrOperationClaimDynamic;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Domain.Entities;

namespace Application.Features.UserOperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SiteUser, UserOperationClaimListViewDto>().ForMember(opt => opt.UserID, src => src.MapFrom(x => x.Id))
                .ForMember(opt => opt.UserName, src => src.MapFrom(x => x.FirstName + ' ' + x.LastName))
                .ForMember(opt => opt.Claims, opt => opt.MapFrom(src => src.UserOperationClaims.Select(claim => claim.OperationClaim).ToList()))
                .ReverseMap();


            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
            CreateMap<IPaginate<SiteUser>, CreateUserOperationClaimCommandResponse>().ReverseMap();

            CreateMap<UserOperationClaim, UpdateUserOperationClaimCommand>().ReverseMap();
            CreateMap<IPaginate<SiteUser>, UpdateUserOperationClaimCommandResponse>().ReverseMap();

            CreateMap<DeleteOperationClaimCommand, DeleteUserOperationClaimCommand>().ReverseMap();
            CreateMap<IPaginate<SiteUser>, DeleteUserOperationClaimResponse>().ReverseMap();


            CreateMap<IPaginate<SiteUser>, GetListUserOperationClaimQueryResponse>().ReverseMap();
            CreateMap<IPaginate<SiteUser>, GetListUserOperationClaimDynamicQueryResponse>().ReverseMap();
        }
    }
}
