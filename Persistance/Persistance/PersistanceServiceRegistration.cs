using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Context;
using Persistance.Repositories;

namespace Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
            services.AddScoped<IOperationClaimRepostiory, OperationClaimRepository>();
            services.AddScoped<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddScoped<ISiteUserRepository, SiteUserRepository>();
            services.AddScoped<IForgotPasswordRepository, ForgotPasswordRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReportTypeRepository, ReportTypeRepository>();
            services.AddScoped<IBanRepository, BanRepository>();
            services.AddDbContext<EFDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("UnitedDb")));

            return services;
        }
    }
}
