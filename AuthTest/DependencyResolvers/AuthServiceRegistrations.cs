using Application.Features.Auth.Commands.ForgetPassword;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Microsoft.Extensions.DependencyInjection;

namespace AuthTest.DependencyResolvers
{
    public static class AuthServiceRegistrations
    {
        public static void AddAuthServices(this IServiceCollection services)
        {

            services.AddTransient<LoginCommand>();
            services.AddTransient<RegisterCommand>();
            services.AddTransient<ForgetPasswordCommand>();

        }
    }
}
