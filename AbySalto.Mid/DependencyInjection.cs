using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using FluentValidation;
using MediatR;
using Serilog;
using AbySalto.Mid.Application;
using AbySalto.Mid.OptionsSetup;
using AbySalto.Mid.Application.Behaviors;
using AbySalto.Mid.WebApi.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AbySalto.Mid
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddOpenApi();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AbySalto", Version = "v1" });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.ConfigureOptions<EmailOptionsSetup>();
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingPipelineBehavior<,>));

            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationPipelineBehavior<,>));

            services.AddValidatorsFromAssembly(AssemblyReference.Assembly,
                includeInternalTypes: true);

            return services;
        }
    }
}
