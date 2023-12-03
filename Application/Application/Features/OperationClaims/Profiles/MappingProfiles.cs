using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.DeleteOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Queries.GetListOperationClaim;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace Application.Features.OperationClaims.Profiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, CreateOperationClaimResponse>().ReverseMap();


            CreateMap<OperationClaim, DeleteOperationClaimResponse>().ReverseMap();


            CreateMap<OperationClaim, UpdateOperationClaimCommand>().ReverseMap();
            CreateMap<OperationClaim, UpdateOperationClaimCommandResponse>().ReverseMap();

            CreateMap<OperationClaim, OperationClaimListViewDto>().ReverseMap();
            CreateMap<IPaginate<OperationClaim>, GetListOperationClaimQueryResponse>().ReverseMap();
            

        }
    }
}
