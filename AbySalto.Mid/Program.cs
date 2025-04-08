
using AbySalto.Mid.Application;
using AbySalto.Mid.Infrastructure;
using AbySalto.Mid.Middleware;
using Serilog;

namespace AbySalto.Mid
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddPresentation()
                .AddInfrastructure(builder.Configuration)
                .AddApplication();

            builder.Services.AddControllers();
            builder.Services.AddOpenApi();

            builder.Host.UseSerilog((context, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Desk Link");
                    options.RoutePrefix = string.Empty;
                });
            }

            app.UseSerilogRequestLogging();

            app.UseCors(builder => builder
                 .AllowAnyOrigin()
                 //.WithOrigins("http://localhost:3000", "https://Authentication-web.azurewebsites.net")
                 .AllowAnyMethod()
                 .AllowAnyHeader());


            app.UseMiddleware(typeof(ErrorHandlerMiddleware));

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
