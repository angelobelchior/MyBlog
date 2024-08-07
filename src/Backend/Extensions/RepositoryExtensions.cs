using Microsoft.EntityFrameworkCore;
using MyBlog.Backend.Repositories;
using MyBlog.Backend.Repositories.Posts;

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
        
        services.AddScoped<PostsRepository>();
        services.AddScoped<IPostsRepository, PostsRepositoryDecorator>();
    }
}