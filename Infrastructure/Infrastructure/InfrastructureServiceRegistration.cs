using Infrastructure.IpStack.Abstract;
using Infrastructure.IpStack.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IIpStackService, IpStackService>();
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = configuration.GetValue<string>("Google:web:client_id");
                googleOptions.ClientSecret = configuration.GetValue<string>("Google:web:client_secret");
            });

            services.AddAuthentication().AddFacebook(facebook =>
            {
                facebook.AppId= configuration["Authentication:Facebook:AppId"];
                facebook.AppSecret= configuration["Authentication:Facebook:AppSecret"];
            });

            return services;
        }
    }
}
