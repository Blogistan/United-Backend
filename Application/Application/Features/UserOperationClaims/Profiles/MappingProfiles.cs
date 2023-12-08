using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim;
using Application.Features.UserOperationClaims.Dtos;
using Application.Features.UserOperationClaims.Queries.GetListUserOperationClaims;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.UserOperationClaims.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserOperationClaim, UserOperationClaimListViewDto>().ForMember(opt => opt.UserID, src => src.MapFrom(x => x.UserId))
                .ForMember(opt => opt.UserName, src => src.MapFrom(x => x.User.FirstName + ' ' + x.User.LastName))
                .ForMember(opt => opt.Claims, src => src.MapFrom(x => x.OperationClaim))
                .ReverseMap();

            CreateMap<UserOperationClaim, CreateUserOperationClaimCommand>();
            CreateMap<UserOperationClaim, CreateUserOperationClaimCommandResponse>();

            CreateMap<UserOperationClaim, UpdateOperationClaimCommand>();
            CreateMap<UserOperationClaim, UpdateOperationClaimCommandResponse>();

            CreateMap<DeleteOperationClaimCommand, CreateUserOperationClaimCommand>();
            CreateMap<DeleteOperationClaimCommand, CreateUserOperationClaimCommandResponse>();

            CreateMap<IPaginate<UserOperationClaim>, GetListUserOperationClaimQueryResponse>().ReverseMap();
        }
    }
}
