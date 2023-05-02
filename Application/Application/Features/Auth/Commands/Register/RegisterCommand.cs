using Application.Features.Auth.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<RegisteredResponse>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }
        public string IpAddress { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
        {
            private readonly ISiteUserRepository siteUserRepository;
            private readonly AuthBussinessRules authBussinessRules;
            private readonly IAuthService authService;
            private readonly IMapper mapper;
            public RegisterCommandHandler(ISiteUserRepository siteUserRepository, AuthBussinessRules authBussinessRules, IAuthService authService, IMapper mapper)
            {
                this.mapper = mapper;
                this.siteUserRepository = siteUserRepository;
                this.authBussinessRules = authBussinessRules;
                this.authService = authService;
            }

            public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                await authBussinessRules.UserEmailCannotBeDuplicatedWhenInserted(request.UserForRegisterDto.Email);

                byte[] hash, salt;
                HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out hash, out salt);

                SiteUser siteUser = mapper.Map<SiteUser>(request.UserForRegisterDto);

                siteUser.PasswordHash = hash;
                siteUser.PasswordSalt = salt;
                siteUser.Status = true;

                await siteUserRepository.AddAsync(siteUser);

                AccessToken accessToken = await authService.CreateAccessToken(siteUser);
                RefreshToken refreshToken = await authService.CreateRefreshToken(siteUser, request.IpAddress);
                await authService.AddRefreshToken(refreshToken);

                RegisteredResponse registeredResponse = new()
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };

                return registeredResponse;



            }
        }
    }
}
