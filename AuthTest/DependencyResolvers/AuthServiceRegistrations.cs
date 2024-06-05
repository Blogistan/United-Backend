using Application.Features.Auth.Commands.ForgetPassword;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
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
            services.AddTransient<ForgotPasswordFakeData>();
            services.AddTransient<BanFakeData>();
            services.AddTransient<CategoryFakeData>();
            services.AddTransient<EmailAuthenticatorFakeData>();
            services.AddTransient<OtpAuthenticatorFakeData>();
            services.AddTransient<UserOperationClaimFakeData>();
            services.AddTransient<RefreshTokenFakeData>();
            services.AddTransient<LoginCommand>();
            services.AddTransient<RegisterCommand>();
            services.AddTransient<ForgetPasswordCommand>();
            
        }
    }
}
