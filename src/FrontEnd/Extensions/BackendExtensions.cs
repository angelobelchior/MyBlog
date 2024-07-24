using Polly;
using Polly.Extensions.Http;

namespace MyBlog.Frontend.Extensions;

public static class BackendExtensions
{
    public static void AddBackend(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient("MyBlog.Backend")
            .ConfigureHttpClient((_, httpClient) =>
            {
                var uri = configuration["Backend:Uri"]!;
                var timeout = int.Parse(configuration["Backend:TimeoutInSeconds"]!);
                
                httpClient.BaseAddress = new Uri(uri);
                httpClient.Timeout = TimeSpan.FromSeconds(timeout);
            }).AddPolicyHandler(HttpPolicyExtensions.HandleTransientHttpError().RetryAsync());
    }
}