using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyBlog.Repositories;

namespace MyBlog.Pages;

public class ListModel(
    MyBlogDbContext dbContext) 
    : PageModel
{
    private const int PageSize = 3;

    public int? CategoryId { get; set; } = null;
    public int? TagId { get; set; } = null;
    public int TotalPages { get; set; }
    public int PageIndex { get; set; }
    public Models.Post[] Posts { get; set; } = [];
    
    public void OnGet(
        int pageIndex = 1,
        int? categoryId = null, 
        int? tagId = null)
    {
        PageIndex = pageIndex;
        CategoryId = categoryId;
        TagId = tagId;
        
        var posts = dbContext.GetPostQueryable();
        if (categoryId is not null)
            posts = posts.Where(p => p.Category.CategoryID == CategoryId);
        if (tagId is not null)
            posts = posts.Where(p => p.Tags.Any(t => t.TagID == TagId));
        
        var totalPosts = posts.Count();
        TotalPages = (int)Math.Ceiling(totalPosts / (double)PageSize);

        Posts = posts
            .OrderByDescending(p => p.CreatedAt)
            .Skip((pageIndex - 1) * PageSize)
            .Take(PageSize)
            .Select(p => p.ToModel())
            .ToArray();
    }
}