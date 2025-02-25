﻿using Application.Features.UserOperationClaims.Dtos;
using Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Commands.CreateUserOperationClaim
{
    public record CreateUserOperationClaimCommandResponse :IResponse
    {
        public List<UserOperationClaimListViewDto> Items { get; set; }

        public CreateUserOperationClaimCommandResponse(List<UserOperationClaimListViewDto> items)
        {
            Items = items;
        }
    }
}
