using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class BanCheckBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IBanRepository _banRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BanCheckBehavior(IBanRepository banRepository, IHttpContextAccessor httpContextAccessor)
    {
        _banRepository = banRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim != null && int.TryParse(userIdClaim, out var userId))
        {
            var isBanned = await CheckIfUserIsBannedAsync(_banRepository, userId);

            if (isBanned)
            {
                throw new BanException("User banned.");
            }
        }

        return await next();
    }
    private async Task<bool> CheckIfUserIsBannedAsync(IBanRepository banRepository, int userId)
    {
        var ban = await banRepository.GetAsync(x => x.SiteUserId == userId);

        if (ban != null && (ban.IsPerma || (ban.BanStartDate <= DateTime.UtcNow && ban.BanEndDate >= DateTime.UtcNow)))
            return true;

        return false;
    }
}
