using Application.Features.SiteUsers.Commands.CreateSiteUser;
using Application.Features.SiteUsers.Commands.DeleteSiteUser;
using Application.Features.SiteUsers.Commands.UpdateSiteUser;
using Application.Features.SiteUsers.Queries.GetById;
using Application.Features.SiteUsers.Queries.GetList;
using AuthTest.Mocks.FakeDatas;
using Microsoft.Extensions.DependencyInjection;

namespace AuthTest.DependencyResolvers
{
    public static class UsersTestServiceRegistration
    {
        public static void AddUserServices(this IServiceCollection services)
        {
            services.AddTransient<SiteUserFakeData>();
            services.AddTransient<CreateSiteUserCommand>();
            services.AddTransient<UpdateSiteUserCommand>();
            services.AddTransient<DeleteSiteUserCommand>();
            services.AddTransient<GetByIdSiteUserQuery>();
            services.AddTransient<GetListSiteUserQuery>();
            services.AddSingleton<CreateSiteUserCommandValidator>();
            services.AddSingleton<UpdateSiteUserCommandValidator>();
        }
    }
}
