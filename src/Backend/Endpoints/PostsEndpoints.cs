using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Backend.Repositories;
using MyBlog.Shared.Models;

namespace MyBlog.Backend.Endpoints;

public static class PostsEndpoints
{
    public static void UsePostEndpoints(this WebApplication app)
    {
        app.MapGet("/posts", async (
                [FromServices] MyBlogDbContext context,
                CancellationToken cancellationToken,
                int pageIndex = 1,
                int? categoryId = null,
                int? tagId = null) =>
            {
                const int pageSize = 3;

                var posts = context!.GetPostQueryable();
                if (categoryId is not null)
                    posts = posts.Where(p => p.Category.CategoryID == categoryId);
                if (tagId is not null)
                    posts = posts.Where(p => p.Tags.Any(t => t.TagID == tagId));

                var totalPosts = posts.Count();
                var totalPages = (int)Math.Ceiling(totalPosts / (double)pageSize);
                app.Logger.LogInformation("Total posts: {TotalPosts}. Total pages: {TotalPages}", totalPosts,
                    totalPages);

                var allPosts = await posts
                    .OrderByDescending(p => p.CreatedAt)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => p.ToModel())
                    .ToListAsync(cancellationToken);

                return Results.Ok(new Result<List<Post>>()
                {
                    Data = allPosts,
                    Pagination = new()
                    {
                        PageIndex = pageIndex,
                        PageSize = pageSize,
                        TotalPages = totalPages,
                        TotalItems = totalPosts
                    }
                });
            })
            .WithName("AllPosts")
            .WithOpenApi();

        app.MapGet("/posts/newest", async (
                [FromServices] MyBlogDbContext context,
                CancellationToken cancellationToken,
                int maxCount = 5) =>
            {
                var posts = await context.GetPostQueryable()
                    .OrderByDescending(p => p.CreatedAt)
                    .Take(maxCount)
                    .Select(p => p.ToModel())
                    .ToListAsync(cancellationToken);

                return Results.Ok(new Result<List<Post>> { Data = posts });
            })
            .WithName("NewestPosts")
            .WithOpenApi();

        app.MapGet("/posts/{id:int}", async (
                [FromServices] MyBlogDbContext context,
                CancellationToken cancellationToken,
                int id) =>
            {
                var post = await context.GetPostQueryable()
                    .FirstOrDefaultAsync(p => p.PostID == id, cancellationToken);
                return post is null
                    ? Results.NotFound()
                    : Results.Ok(new Result<Post> { Data = post.ToModel() });
            })
            .WithName("PostById")
            .WithOpenApi();
    }
}