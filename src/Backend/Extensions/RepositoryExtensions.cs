using Microsoft.EntityFrameworkCore;
using MyBlog.Backend.Repositories;

namespace MyBlog.Backend.Extensions;

public static class RepositoryExtensions
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MyBlogDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServerConnectionString")!);
#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        });
    }
}