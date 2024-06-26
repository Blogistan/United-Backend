using AuthTest.Mocks.FakeDatas;
using Microsoft.Extensions.DependencyInjection;

namespace AuthTest.DependencyResolvers
{
    public static class TestServiceRegistration
    {
        public static void AddTestServices(this IServiceCollection services)
        {
            services.AddTransient<CategoryFakeData>();
            services.AddTransient<CommentFakeData>();
            services.AddTransient<ContentFakeData>();
            services.AddTransient<BlogFakeData>();
            services.AddTransient<SiteUserFakeData>();
            services.AddTransient<OperationClaimFakeData>();
            services.AddTransient<ForgotPasswordFakeData>();
            services.AddTransient<BanFakeData>();
            services.AddTransient<ReportFakeData>();
            services.AddTransient<EmailAuthenticatorFakeData>();
            services.AddTransient<OtpAuthenticatorFakeData>();
            services.AddTransient<UserOperationClaimFakeData>();
            services.AddTransient<RefreshTokenFakeData>();
            services.AddTransient<ReportTypeFakeData>();
        }

    }
}
