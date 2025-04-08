using AbySalto.Mid.Application.Abstractions;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;
using AbySalto.Mid.Infrastructure.Database;
using AbySalto.Mid.Infrastructure.Options.Authentication;
using AbySalto.Mid.Infrastructure.Repository;
using AbySalto.Mid.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AbySalto.Mid.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration)
                .AddServices()
                .AddRepositories()
                .AddProviders();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IVerificationCodeService, VerificationCodeService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddHttpClient<IExternalApiService, ExternalApiService>(client =>
            {
                client.BaseAddress = new Uri("https://dummyjson.com/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return services;
        }
        private static IServiceCollection AddProviders(this IServiceCollection services)
        {

            // Providers
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IJwtProvider, JwtProvider>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IDimensionsRepository, DimensionsRepository>();
            services.AddScoped<IMetaRepository, MetaRepository>();
            services.AddScoped<ICartRepository, CartRepository>();

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("Default"),
                    ctxOptions =>
                    {
                        ctxOptions.MigrationsAssembly("AbySalto.Mid.Infrastructure");
                        ctxOptions.CommandTimeout(180);
                    }
                );
                options.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            });
            return services;
        }
    }
}
