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

            var elasticsearchUri = new Uri(configuration["Elasticsearch:Uri"]!);
            options.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(elasticsearchUri)
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"{configuration["Elasticsearch:IndexFormatPrefix"]}-{{0:yyyy.MM.dd}}",
                NumberOfReplicas = 1,
                NumberOfShards = 2,
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
            .WithTracing(tracing =>
            {
                tracing.SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(appName, appVersion));

                tracing.AddAspNetCoreInstrumentation();
                tracing.AddHttpClientInstrumentation();
                tracing.AddSqlClientInstrumentation();
                tracing.AddOtlpExporter();
#if DEBUG
                tracing.AddConsoleExporter();
#endif
            })
            .WithMetrics(metrics =>
            {
                metrics.SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(appName, appVersion));

                metrics.AddRuntimeInstrumentation();
                metrics.AddHttpClientInstrumentation();
                metrics.AddAspNetCoreInstrumentation();
                
                metrics.AddOtlpExporter();
#if DEBUG
               metrics.AddConsoleExporter();
#endif
            });
    }
}