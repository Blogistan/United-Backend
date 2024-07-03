using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
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
            CreateMap<User, UserOperationClaimListViewDto>().ForMember(opt => opt.UserID, src => src.MapFrom(x => x.Id))
                .ForMember(opt => opt.UserName, src => src.MapFrom(x => x.FirstName + ' ' + x.LastName))
                .ForMember(opt => opt.Claims, opt => opt.MapFrom(src => src.UserOperationClaims.Select(claim => claim.OperationClaim).ToList()))
                .ReverseMap();


            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>().ReverseMap();
            CreateMap<IPaginate<User>, CreateUserOperationClaimCommandResponse>().ReverseMap();

            CreateMap<UserOperationClaim, UpdateUserOperationClaimCommand>().ReverseMap();
            CreateMap<IPaginate<User>, UpdateUserOperationClaimCommandResponse>().ReverseMap();

            CreateMap<DeleteOperationClaimCommand, DeleteUserOperationClaimCommand>().ReverseMap();
            CreateMap<IPaginate<User>, DeleteUserOperationClaimResponse>().ReverseMap();


            CreateMap<IPaginate<User>, GetListUserOperationClaimQueryResponse>().ReverseMap();
            CreateMap<IPaginate<User>, GetListUserOperationClaimDynamicQueryResponse>().ReverseMap();
        }
    }
}
