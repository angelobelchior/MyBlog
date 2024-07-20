using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using MyBlog.Repositories;

namespace MyBlog.Pages;

public class PostModel(
    MyBlogDbContext dbContext) 
    : PageModel
{
    public Models.Post? Post { get; set; }
    
    [OutputCache]
    public void OnGet(int id)
    {
        var posts = dbContext.GetPostQueryable();
        Post = posts.FirstOrDefault(p => p.PostID == id)?.ToModel();
    }
}