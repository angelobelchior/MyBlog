using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OutputCaching;
using MyBlog.Repositories;

namespace MyBlog.Pages;

public class IndexModel(
    ILogger<IndexModel> logger,
    MyBlogDbContext dbContext)
    : PageModel
{
    public Models.Post? RecentPost { get; set; }
    public Models.Post[] LatestPosts { get; set; } = [];

    [OutputCache]
    public void OnGet()
    {
        var meuObjeto = new UserInfo { Username = "angelo", Password = "abc123" };

        logger.LogInformation("Username: {Username}", meuObjeto.Username);

        var posts = dbContext.GetPostQueryable()
            .OrderByDescending(p => p.CreatedAt)
            .Take(6)
            .Select(p => p.ToModel())
            .ToArray();;

        RecentPost = posts.FirstOrDefault();
        LatestPosts = posts.Skip(1).ToArray();
    }

    private class UserInfo
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}