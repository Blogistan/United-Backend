using Infrastructure.IpStack.Abstract;
using Infrastructure.IpStack.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IIpStackService, IpStackService>();
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = configuration.GetValue<string>("Authentication:Google:client_id");
                googleOptions.ClientSecret = configuration.GetValue<string>("Authentication:Google:client_secret");
            });

            services.AddAuthentication().AddFacebook(facebook =>
            {
                facebook.AppId = configuration["Authentication:Facebook:AppId"];
                facebook.AppSecret = configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddAuthentication().AddTwitter(twitter =>
            {
                twitter.ConsumerKey = configuration["Authentication:Twitter:ConsumerAPIKey"];
                twitter.ConsumerSecret = configuration["Authentication:Twitter:ConsumerSecret"];
                twitter.RetrieveUserDetails = true;
                
            });

            services.AddAuthentication().AddOAuth("Github", options =>
            {
                options.ClientId = configuration.GetValue<string>("Authentication:Github:client_id");
                options.ClientSecret = configuration.GetValue<string>("Authentication:Github:client_secret");
                options.CallbackPath = new PathString("sigin-github");
                options.ClaimsIssuer = "OAuth2-Github";
                options.SaveTokens = true;
                options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                options.UserInformationEndpoint = "https://api.github.com/user";
            });
            return services;
        }
    }
}
