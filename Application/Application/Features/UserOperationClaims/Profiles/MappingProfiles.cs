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
            CreateMap<UserOperationClaim, UserOperationClaimListViewDto>().ForMember(opt => opt.UserID, src => src.MapFrom(x => x.UserId))
                .ForMember(opt => opt.UserName, src => src.MapFrom(x => x.User.FirstName + ' ' + x.User.LastName))
                .ForMember(opt => opt.Claims, opt => opt.MapFrom(src =>new List<OperationClaim> { src.OperationClaim}))
                .ReverseMap();




            //CreateMap<SiteUser, UserOperationClaimListViewDto>().ForMember(opt => opt.UserID, src => src.MapFrom(x => x.Id))
            //    .ForMember(opt => opt.UserName, src => src.MapFrom(x => x.FirstName + ' ' + x.LastName))
            //    .ForMember(opt => opt.Claims, opt => opt.MapFrom(src => new List<OperationClaim>() { src.UserOperationClaims.Select(x => x.OperationClaim).ToList() }))
            //    .ReverseMap();

            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
            CreateMap<IPaginate<UserOperationClaim>, CreateUserOperationClaimCommandResponse>().ReverseMap();

            CreateMap<UserOperationClaim, UpdateUserOperationClaimCommand>().ReverseMap();
            CreateMap<UserOperationClaim, UpdateOperationClaimCommandResponse>().ReverseMap();

            CreateMap<DeleteOperationClaimCommand, DeleteUserOperationClaimCommand>().ReverseMap();
            CreateMap<DeleteOperationClaimCommand, DeleteUserOperationClaimResponse>().ReverseMap();

            //CreateMap<IPaginate<UserOperationClaim>, GetListUserOperationClaimQueryResponse>().ReverseMap();
            //CreateMap<IPaginate<UserOperationClaim>, GetListUserOperationClaimDynamicQueryResponse>().ReverseMap();


            CreateMap<IPaginate<SiteUser>, GetListUserOperationClaimQueryResponse>().ReverseMap();
        }
    }
}
