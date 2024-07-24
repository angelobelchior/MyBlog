namespace MyBlog.Frontend.Extensions;

public static class HealthChecksExtensions
{
    public static void AddAllHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("RedisConnectionString")!;

        services.AddHealthChecks()
            .AddRedis(name: "Redis", 
                      redisConnectionString: redisConnectionString, 
                      tags: new[] { "cache" });
    }
}