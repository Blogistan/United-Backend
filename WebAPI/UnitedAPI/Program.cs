﻿using Application;
using Application.Services.Assistant.Hubs;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security;
using Core.Security.Encryption;
using Core.Security.JWT;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

// Add services to the container.

builder.Services.AddSecurityServices();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowAnyOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    c.AddPolicy("AllowLocalAngularClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // spesifik origin
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // SignalR için lazım
    });

});

builder.Services.AddApplicationServices();
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();


builder.Services.AddRateLimiter(rateLimitOptions =>
{
    rateLimitOptions.AddConcurrencyLimiter("Concurrency", options =>
    {
        options.PermitLimit = 4;
        options.QueueLimit = 2;
        options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
    });
    rateLimitOptions.OnRejected = (context, CancellationToken) =>
    {
        //Log Operations...
        return new();
    };
});

builder.Services.AddRateLimiter(rateLimitOptions =>
{
    rateLimitOptions.AddSlidingWindowLimiter("Slider", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 10;
        options.SegmentsPerWindow = 2;
        options.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 5;
    });

    rateLimitOptions.OnRejected = (context, CancellationToken) =>
    {
        //Log Operations...
        return new();
    };
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Esquetta", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    // OAuth2 authentication configuration
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://accounts.google.com/o/oauth2/auth"),
                TokenUrl = new Uri("https://accounts.google.com/o/oauth2/token"),
                Scopes = new Dictionary<string, string>
                {
                    { "openid", "OpenID Scope" },
                    { "profile", "Profile Scope" },
                    { "email", "Email Scope" }
                }
            }
        }
    });

    // Requirements for displaying "Authorize" on Swagger UI
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference{Type=ReferenceType.SecurityScheme,Id="oauth2"}
            },
            new[]{"openid","profile","email"}// Allowed OAuth2 scopes
        }

    });


});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            //LifetimeValidator=(notbefore,expires,securityToken,validationParameter)=> expires!=null?expires>DateTime.Now:false


        };
    });


var app = builder.Build();

app.MapDefaultEndpoints();
app.Map("/", () => { }).RequireRateLimiting("...");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapHub<AiHub>("ai-hub").RequireCors("AllowLocalAngularClient");
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthorization();
app.UseCors("AllowAnyOrigin");
app.MapControllers();

app.Run();



