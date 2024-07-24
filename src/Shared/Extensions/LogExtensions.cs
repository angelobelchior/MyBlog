using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace MyBlog.Shared.Extensions;

public static class LogExtensions
{
    public static void AddLog(
        this IServiceCollection services,
        IConfiguration configuration,
        string appName,
        string appVersion)
    {
        services.AddSerilog((_, options) =>
        {
            options.WriteTo.Console();

            var elasticSearchEndpoint = new Uri(configuration["Elasticsearch:Uri"]!);
            options.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticSearchEndpoint)
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{configuration["Elasticsearch:IndexFormatPrefix"]!}-{DateTime.UtcNow:yyyy-MM}",
                NumberOfReplicas = int.Parse(configuration["Elasticsearch:NumberOfReplicas"]!),
                NumberOfShards = int.Parse(configuration["Elasticsearch:NumberOfShards"]!),
            });

            options.MinimumLevel.Debug();

            options.Enrich.FromLogContext();
            options.Enrich.WithMemoryUsage();
            options.Enrich.WithThreadId();
            options.Enrich.WithProcessId();
            options.Enrich.WithProcessName();
            options.Enrich.WithEnvironmentUserName();
            options.Enrich.WithProperty("Application", appName);
            options.Enrich.WithProperty("Version", appVersion);
            options.Enrich.WithExceptionDetails();
        });

        services.AddOpenTelemetry(appName, appVersion);
    }

    public static void UseLog(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();
    }

    private static void AddOpenTelemetry(
        this IServiceCollection services,
        string appName,
        string appVersion)
    {
        services.AddOpenTelemetry()
            .WithTracing(trace =>
            {
                trace
                    .SetResourceBuilder(
                        ResourceBuilder.CreateDefault()
                            .AddService(serviceName: appName,
                                serviceVersion: appVersion))
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation()
                    .AddOtlpExporter()
                    .AddConsoleExporter();
            })
            .WithMetrics(metrics =>
            {
                metrics
                    .SetResourceBuilder(
                        ResourceBuilder.CreateDefault()
                            .AddService(serviceName: appName,
                                serviceVersion: appVersion))
                    .AddRuntimeInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter()
                    .AddConsoleExporter();
            });
    }
}