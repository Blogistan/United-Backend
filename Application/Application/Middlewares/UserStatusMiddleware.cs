using Application.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

public class UserStatusMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UserStatusMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userIdClaim = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userIdClaim != null && int.TryParse(userIdClaim, out var userId))
        {
            // Her istek için yeni bir scope oluşturun
            using var scope = _serviceScopeFactory.CreateScope();
            var banRepository = scope.ServiceProvider.GetRequiredService<IBanRepository>();

            var isBanned = await CheckIfUserIsBannedAsync(banRepository, userId);

            if (isBanned)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Bu kullanıcı yasaklanmıştır.");
                return;
            }
        }

        await _next(context);
    }

    private async Task<bool> CheckIfUserIsBannedAsync(IBanRepository banRepository, int userId)
    {
        var ban = await banRepository.GetAsync(x => x.ReportId == userId);

        if (ban != null && (ban.IsPerma || (ban.BanStartDate <= DateTime.UtcNow && ban.BanEndDate >= DateTime.UtcNow)))
            return true;

        return false;
    }
}
