namespace MyBlog.Backend.Extensions;

public static class CacheExtensions
{
    public static void AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnectionString");
            options.InstanceName = "MyBlogBackendInstance";
        });
        services.AddDistributedMemoryCache();
    }
}