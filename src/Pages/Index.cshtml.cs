using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OutputCaching;
using MyBlog.Repositories;

namespace MyBlog.Pages;

public class IndexModel(
    MyBlogDbContext dbContext)
    : PageModel
{
    public Models.Post? RecentPost { get; set; }
    public Models.Post[] LatestPosts { get; set; } = [];

    [OutputCache]
    public void OnGet()
    {
        var posts = dbContext.GetPostQueryable();

        RecentPost = posts
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => p.ToModel())
            .FirstOrDefault();

        LatestPosts = posts
            .OrderByDescending(p => p.CreatedAt)
            .Take(5)
            .Select(p => p.ToModel())
            .ToArray();
    }
}