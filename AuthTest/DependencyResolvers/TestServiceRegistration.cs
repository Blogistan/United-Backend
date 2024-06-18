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
        }

    }
}
