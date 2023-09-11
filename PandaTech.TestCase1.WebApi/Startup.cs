using System.Reflection;
using FluentValidation;
using PandaTech.TestCase1.Configuration;
using PandaTech.TestCase1.Extensions;
using Serilog;

namespace PandaTech.TestCase1.WebApi;

public static class Startup
{
    public static IServiceCollection Initialize(this IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
        });

        services.ConfigureApplicationCookie(config =>
        {
            config.Cookie.Name = $"{EnvSettings.Namespace}.Cookie";
        });
        
        services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssemblies(new [] { Assembly.GetExecutingAssembly() });

        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });
        });
        
        services.AddSwaggerGen();
        
        return services;
    }

    public static void Initialize(this WebApplication app)
    {
        app.UseCors();

        app.UseMiddleware<AuthenticationMiddleware>();
        
        app.UseRouting();
        
        app.UseSerilogRequestLogging();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapDefaultControllerRoute();
    }
}