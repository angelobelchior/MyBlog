namespace MyBlog.Frontend.Extensions;

public static class CacheExtensions
{
    public static void AddRedisAddOutputCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("RedisConnectionString")!;
        services.AddStackExchangeRedisOutputCache(options =>
        {
            options.Configuration = redisConnectionString;
            options.InstanceName = "MyBlogFrontendInstance";
        });
        services.AddOutputCache(options => { options.AddBasePolicy(p => p.Expire(TimeSpan.FromSeconds(1))); });
    }
}