using Application.Services.Auth;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.Auth.Commands.ExternalLoginUrl.Github
{
    public class GetGithubLoginLinkCommand : IRequest<GetGithubLoginLinkCommandResponse>
    {       

        public class GetGithubLoginLinkCommandHandler : IRequestHandler<GetGithubLoginLinkCommand, GetGithubLoginLinkCommandResponse>
        {
            private readonly IAuthService authService;
            public GetGithubLoginLinkCommandHandler(IAuthService authService)
            {

                this.authService = authService;
            }
            public async Task<GetGithubLoginLinkCommandResponse> Handle(GetGithubLoginLinkCommand request, CancellationToken cancellationToken)
            {
                var url = await authService.GetGithubLoginLink();
                return new GetGithubLoginLinkCommandResponse { Url = url.Url };
            }
        }
    }
}
