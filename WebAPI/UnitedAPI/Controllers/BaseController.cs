using Core.Security.Entities;
using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UnitedAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IMediator? Mediator => mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private IMediator? mediator;

        protected string GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Fowarded-For"))
                return Request.Headers["X-Fowarded-For"].ToString();

            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        protected string? GetRefreshTokenFromCookie()
        {
            return Request.Cookies["refreshToken"];
        }
        protected void SetRefreshTokenToCookie(RefreshToken refreshToken)
        {
            Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
        }
        protected int GetUserUdFromToken()
        {
            return HttpContext.User.GetUserId();
                
        }

    }
}
