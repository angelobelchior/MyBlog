using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;

namespace MyBlog.Shared.Extensions;

public static class HealthChecksExtensions
{   public static void UseHealthCheckEndpoints(this WebApplication app)
    {
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
    }
}