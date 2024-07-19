using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace MyBlog.ExtensionMethods;

public static class HealthChecksExtensions
{
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

        app.UseHealthChecks("/health/tag/cache", new HealthCheckOptions
        {
            Predicate = (registration) => registration.Tags.Contains("cache")
        });

        app.UseHealthChecks("/health/tag/database", new HealthCheckOptions
        {
            Predicate = (registration) => registration.Tags.Contains("database")
        });

    }
}