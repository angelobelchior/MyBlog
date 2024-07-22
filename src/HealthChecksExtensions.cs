using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MyBlog;

public static class HealthChecksExtensions
{
    public static void AddAllHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlServerConnectionString = configuration.GetConnectionString("SqlServerConnectionString")!;
        var redisConnectionString = configuration.GetConnectionString("RedisConnectionString")!;

        services.AddHealthChecks()
            .AddSqlServer(name: "SqlServer", 
                          connectionString: sqlServerConnectionString, 
                          tags: new[] { "database" },
                          /*healthQuery: "exec proc @abc", */
                          failureStatus: HealthStatus.Degraded)
            
            .AddRedis(name: "Redis", 
                      redisConnectionString: redisConnectionString, 
                      tags: new[] { "cache" });
    }

    public static void UseAllHealthChecks(this WebApplication app)
    {
        //app.MapHealthChecks("health");

        app.UseHealthChecks("/health/liveness", new HealthCheckOptions
        {
            Predicate = _ => false
        });

        app.UseHealthChecks("/health/readiness");

        app.UseHealthChecks("/health/readiness/details", new HealthCheckOptions
        {
            ResponseWriter = async (httpContext, report) =>
            {
                httpContext.Response.ContentType = "application/json";
                var json = JsonSerializer.Serialize(new
                {
                    status = report.Status.ToString(),
                    totalDuration = report.TotalDuration,
                    components = report.Entries.Select(entry => new
                    {
                        name = entry.Key,
                        duration = entry.Value.Duration,
                        status = entry.Value.Status.ToString(),
                        exception = entry.Value.Exception?.Message
                    })
                });
                await httpContext.Response.WriteAsync(json);
            }
        });

        // app.UseHealthChecks("/health/tag/cache", new HealthCheckOptions
        // {
        //     Predicate = (registration) => registration.Tags.Contains("cache")
        // });
        //
        // app.UseHealthChecks("/health/tag/database", new HealthCheckOptions
        // {
        //     Predicate = (registration) => registration.Tags.Contains("database")
        // });
    }
}