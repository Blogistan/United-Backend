using Application.Services.Auth;
using MediatR;

namespace Application.Features.Auth.Commands.ExternalLoginUrl.Twitter
{
    public class GetTwitterLoginLinkCommand : IRequest<GetTwitterLoginLinkCommandResponse>
    {
        public class GetTwitterLoginLinkCommandHandler : IRequestHandler<GetTwitterLoginLinkCommand, GetTwitterLoginLinkCommandResponse>
        {
            private readonly IAuthService authService;
            public GetTwitterLoginLinkCommandHandler(IAuthService authService)
            {
                this.authService = authService;
            }

            public async Task<GetTwitterLoginLinkCommandResponse> Handle(GetTwitterLoginLinkCommand request, CancellationToken cancellationToken)
            {
                var url = await authService.GetTwitterLoginUrl();

                return new GetTwitterLoginLinkCommandResponse { Url = url.LoginURL };
            }
        }
    }
}
