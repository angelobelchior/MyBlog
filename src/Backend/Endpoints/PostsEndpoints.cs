using Microsoft.AspNetCore.Mvc;
using MyBlog.Backend.Repositories.Posts;
using MyBlog.Shared.Models;

namespace MyBlog.Backend.Endpoints;

public static class PostsEndpoints
{
    public static void UsePostEndpoints(this WebApplication app)
    {
        app.MapGet("/posts", async (
                [FromServices] IPostsRepository repository,
                CancellationToken cancellationToken,
                int pageIndex = 1,
                int pageSize = 3,
                int? categoryId = null,
                int? tagId = null) =>
            {
                var allPosts = await repository.ListAllAsync(cancellationToken, pageIndex, pageSize, categoryId, tagId);
                return Results.Ok(allPosts);
            })
            .WithName("AllPosts")
            .WithDescription("Get all posts")
            .Produces<Result<IReadOnlyCollection<Post>>>(200)
            .WithOpenApi();

        app.MapGet("/posts/newest", async (
                [FromServices] IPostsRepository repository,
                CancellationToken cancellationToken,
                int maxCount = 5) =>
            {
                var posts = await repository.ListNewestAsync(cancellationToken, maxCount);
                return Results.Ok(posts);
            })
            .WithName("NewestPosts")
            .WithDescription("Get newest posts")
            .Produces<Result<IReadOnlyCollection<Post>>>(200)
            .WithOpenApi();

        app.MapGet("/posts/{id:int}", async (
                [FromServices] IPostsRepository repository,
                CancellationToken cancellationToken,
                int id) =>
            {
                var post = await repository.GetByIdAsync(cancellationToken, id);
                return post.Data is null
                    ? Results.NotFound()
                    : Results.Ok(post);
            })
            .WithName("PostById")
            .WithDescription("Get post by id")
            .Produces<Result<Post>>(200)
            .Produces(404)
            .WithOpenApi();
    }
}