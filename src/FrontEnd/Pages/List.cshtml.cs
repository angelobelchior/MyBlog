using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Shared.Models;

namespace MyBlog.Frontend.Pages;

public class ListModel(
    ILogger<ListModel> logger,
    IHttpClientFactory httpClientFactory) 
    : PageModel
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("MyBlog.Backend");
    
    public int PageIndex { get; set; }
    public int TotalPages { get; set; }

    public int? CategoryId { get; set; }
    public int? TagId { get; set; }
    public IEnumerable<Post> Posts { get; set; } = [];
    
    public async Task OnGet(
        int pageIndex = 1,
        int? categoryId = null, 
        int? tagId = null)
    {
        var pageIndexQuery = $"pageIndex={pageIndex}";
        var categoryQuery = categoryId is not null ? $"&categoryId={categoryId}" : "";
        var tagQuery = tagId is not null ? $"&tagId={tagId}" : "";
        var endpoint = $"/posts/?{pageIndexQuery}{categoryQuery}{tagQuery}";
        var result = await _httpClient.GetFromJsonAsync<Result<IEnumerable<Post>>>(endpoint);

        Posts = result!.Data!;
        PageIndex = pageIndex;
        TotalPages = result.Pagination!.TotalPages;
        CategoryId = categoryId;
        TagId = tagId;
        logger.LogInformation("Total posts: {TotalPosts}", result.Pagination!.TotalItems);
    }
}