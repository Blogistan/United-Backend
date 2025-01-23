using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.Features.Auth.Commands.ExternalLoginUrl.Google
{
    public class GetGoogleClientCommand : IRequest<GetGoogleClientCommandResponse>
    {

        public class GetGoogleClientCommandHandler : IRequestHandler<GetGoogleClientCommand, GetGoogleClientCommandResponse>
        {
            private readonly IConfiguration configuration;
            public GetGoogleClientCommandHandler(IConfiguration configuration)
            {
                this.configuration = configuration;
            }
            public async Task<GetGoogleClientCommandResponse> Handle(GetGoogleClientCommand request, CancellationToken cancellationToken)
            {
                return new GetGoogleClientCommandResponse { Client = configuration["Authentication:Google:client_id"] };
            }
        }
    }
}
