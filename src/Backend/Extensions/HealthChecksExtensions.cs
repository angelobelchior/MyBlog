using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MyBlog.Backend.Extensions;

public static class HealthChecksExtensions
{
    public static void AddAllHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlServerConnectionString = configuration.GetConnectionString("SqlServerConnectionString")!;

        services.AddHealthChecks()
            .AddSqlServer(name: "SqlServer",
                connectionString: sqlServerConnectionString,
                tags: new[] { "database" },
                failureStatus: HealthStatus.Degraded);
    }
}