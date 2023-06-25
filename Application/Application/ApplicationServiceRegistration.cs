using Application.Features.Auth.Rules;
using Application.Features.Blogs.Rules;
using Application.Features.Categories.Rules;
using Application.Features.Comments.Rules;
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
using System.Reflection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(x=>x.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly));

            services.AddScoped<AuthBussinessRules>();
            services.AddScoped<CategoryBusinessRules>();
            services.AddScoped<BlogBusinessRules>();
            services.AddScoped<VideoBusinessRules>();
            services.AddScoped<CommentBusinessRules>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMailService, MailKitMailService>();;

            services.AddSingleton<LoggerServiceBase, FileLogger>();

            services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionScopeBehavior<,>));

            return services;
        }

        public static IServiceCollection AddSubClassesOfType(this IServiceCollection services,Assembly assembly,Type type,Func<IServiceCollection,Type,IServiceCollection>? addWithLifeCycle = null)
        {
            var types = assembly.GetTypes().Where(x=>x.IsSubclassOf(type) && type!=x).ToList();
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
