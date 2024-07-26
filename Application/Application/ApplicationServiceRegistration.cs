using Application.Features.Auth.Rules;
using Application.Features.Blogs.Rules;
using Application.Features.Categories.Rules;
using Application.Features.Comments.Rules;
using Application.Features.Contents.Rules;
using Application.Features.OperationClaims.Rules;
using Application.Features.Reports.Rules;
using Application.Features.ReportTypes.Rules;
using Application.Features.SiteUsers.Rules;
using Application.Features.UserOperationClaims.Rules;
using Application.Features.Videos.Rules;
using Application.Services.Auth;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging.Serilog.Logger;
using Core.Mailing;
using Core.Mailing.MailKitImplementations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Reflection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(configuration =>

                {
                    configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                    configuration.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
                    configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
                    configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
                    configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
                    configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
                    configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
                }
            );

            services.AddScoped<AuthBussinessRules>();
            services.AddScoped<UserBusinessRules>();
            services.AddScoped<CategoryBusinessRules>();
            services.AddScoped<BlogBusinessRules>();
            services.AddScoped<VideoBusinessRules>();
            services.AddScoped<CommentBusinessRules>();
            services.AddScoped<ContentBusinessRules>();
            services.AddScoped<ReportBusinessRules>();
            services.AddScoped<ReportTypeBusinessRules>();
            services.AddScoped<OperationClaimBusinessRules>();
            services.AddScoped<UserOperationClaimBusinessRules>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMailService, MailKitMailService>(); ;

            services.AddSingleton<LoggerServiceBase, MongoDbLogger>();

            services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

            return services;
        }

        public static IServiceCollection AddSubClassesOfType(this IServiceCollection services, Assembly assembly, Type type, Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null)
        {
            var types = assembly.GetTypes().Where(x => x.IsSubclassOf(type) && type != x).ToList();
            foreach (var item in types)
            {
                if (addWithLifeCycle == null)
                {
                    services.AddScoped(item);
                }
                else
                {
                    addWithLifeCycle(services, type);
                }
            }
            return services;
        }
    }
}
