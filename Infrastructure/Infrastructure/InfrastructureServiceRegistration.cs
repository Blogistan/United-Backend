using Infrastructure.IpStack.Abstract;
using Infrastructure.IpStack.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IIpStackService, IpStackService>();

            return services;
        }
    }
}
