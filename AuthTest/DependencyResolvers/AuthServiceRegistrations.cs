using Application.Features.Auth.Commands.Login;
using AuthTest.Mocks.FakeDatas;
using Microsoft.Extensions.DependencyInjection;

namespace AuthTest.DependencyResolvers
{
    public static class AuthServiceRegistrations
    {
        public static void AddAuthServices(this IServiceCollection services)
        {
            services.AddTransient<SiteUserFakeData>();
            services.AddTransient<OperationClaimFakeData>();
            services.AddTransient<BanFakeData>();
            services.AddTransient<UserOperationClaimFakeData>();
            services.AddTransient<RefreshTokenFakeData>();
            services.AddTransient<LoginCommand>();
        }
    }
}
