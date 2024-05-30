using AuthTest.DependencyResolvers;
using Microsoft.Extensions.DependencyInjection;

namespace AuthTest
{
    public sealed class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddUserServices();
            services.AddAuthServices();
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(Startup).Assembly));
        }
    }

}

