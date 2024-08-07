using Microsoft.EntityFrameworkCore;
using MyBlog.Shared.Models;
using Post = MyBlog.Shared.Models.Post;

namespace MyBlog.Backend.Repositories.Posts;

internal class PostsRepository(
    MyBlogDbContext context) : IPostsRepository
{
    public async Task<Result<IReadOnlyCollection<Post>>> ListAllAsync(CancellationToken cancellationToken, int pageIndex = 1, int pageSize = 3, int? categoryId = null, int? tagId = null)
    {
        var posts = context.GetPostQueryable();
        if (categoryId is not null)
            posts = posts.Where(p => p.Category.CategoryID == categoryId);
        if (tagId is not null)
            posts = posts.Where(p => p.Tags.Any(t => t.TagID == tagId));

        var allPosts = await posts
            .OrderByDescending(p => p.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(p => p.ToModel())
            .ToListAsync(cancellationToken);
        
        var totalPosts = allPosts.Count;
        var totalPages = (int)Math.Ceiling(totalPosts / (double)pageSize);
                
        return new Result<IReadOnlyCollection<Post>>()
        {
            Data = allPosts,
            Pagination = new()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalItems = totalPosts
            }
        };
    }

    public async Task<Result<IReadOnlyCollection<Post>>> ListNewestAsync(CancellationToken cancellationToken, int maxCount = 5)
    {
        var newestPosts = await context.GetPostQueryable()
            .OrderByDescending(p => p.CreatedAt)
            .Take(maxCount)
            .Select(p => p.ToModel())
            .ToListAsync(cancellationToken);

        return new Result<IReadOnlyCollection<Post>>
        {
            Data = newestPosts
        };
    }

    public async Task<Result<Post>> GetByIdAsync(CancellationToken cancellationToken, int id)
    {
        var post = await context.GetPostQueryable()
            .FirstOrDefaultAsync(p => p.PostID == id, cancellationToken);
        
        if(post is not null)
            return new Result<Post>
            {
                Data = post.ToModel()
            };

        return new Result<Post>();
    }
}