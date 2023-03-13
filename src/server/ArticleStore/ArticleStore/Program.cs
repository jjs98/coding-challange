using ArticleStore.Services;
using ArticleStore.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ArticleStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerDocument(settings =>
            {
                settings.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "ArticleStore";
                };
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                policy => policy
                .WithOrigins("http://localhost:5000", "http://localhost:4200"));
            });

            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")), ServiceLifetime.Singleton);
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddSingleton<IApplicationDbService, ApplicationDbService>();
            builder.Services.AddSingleton<IArticleService, ArticleService>();

            var app = builder.Build();
            var articleService = app.Services.GetService<IArticleService>();
            var loggingService = app.Services.GetService<ILogger<FetchDataService>>();
            _ = new FetchDataService(articleService, loggingService);

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}