﻿using Application.Features.UserOperationClaims.Dtos;
using Core.Application.Responses;

namespace Application.Features.UserOperationClaims.Commands.UpdateUserOperationClaimCommand
{
    public class UpdateUserOperationClaimCommandResponse:IResponse
    {
        public List<UserOperationClaimListViewDto> Items { get; set; }

        public UpdateUserOperationClaimCommandResponse(List<UserOperationClaimListViewDto> items)
        {
            Items = items;
        }
    }
}
