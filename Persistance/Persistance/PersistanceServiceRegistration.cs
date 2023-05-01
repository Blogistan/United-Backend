﻿using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Context;
using Persistance.Repositories;

namespace Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddSingleton<IBlogRepository, BlogRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<ICommentRepository, CommentRepository>();
            services.AddSingleton<IContentRepository, ContentRepository>();
            services.AddSingleton<IEmailAuthenticatorRepository, EmailAuthenticatorRepository>();
            services.AddSingleton<IOperationClaimRepostiory, OperationClaimRepository>();
            services.AddSingleton<IOtpAuthenticatorRepository, OtpAuthenticatorRepository>();
            services.AddSingleton<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddSingleton<IUserOperationClaimRepository, UserOperationClaimRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IVideoRepository, VideoRepository>();
            services.AddScoped<ISiteUserRepository, SiteUserRepository>();
            services.AddDbContext<EFDbContext>(options=>options.UseSqlServer(configuration.GetConnectionString("UnitedDb")));

            return services;
        }
    }
}
