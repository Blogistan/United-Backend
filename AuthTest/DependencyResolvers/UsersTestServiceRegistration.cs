using AuthTest.Mocks.FakeDatas;
using Microsoft.Extensions.DependencyInjection;

namespace AuthTest.DependencyResolvers
{
    public static class UsersTestServiceRegistration
    {
        public static void AddUserServices(this IServiceCollection services)
        {
            services.AddTransient<SiteUserFakeData>();

            // To Do: SiteUser commands will be created.
            //services.AddTransient<CreateUserCommand>();
            //services.AddTransient<UpdateUserCommand>();
            //services.AddTransient<DeleteUserCommand>();
            //services.AddTransient<GetByIdUserQuery>();
            //services.AddTransient<GetListUserQuery>();
            //services.AddSingleton<CreateUserCommandValidator>();
            //services.AddSingleton<UpdateUserCommandValidator>();
        }
    }
}
