using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OutputCaching;
using MyBlog.Shared.Models;

namespace MyBlog.Frontend.Pages;

public class IndexModel(
    ILogger<ListModel> logger,
    IHttpClientFactory httpClientFactory) 
    : PageModel
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("MyBlog.Backend");
    public Post? RecentPost { get; set; }
    public Post[] LatestPosts { get; set; } = [];

    [OutputCache]
    public async Task OnGet()
    {
        var endpoint = $"/posts/newest/?maxCount=6";
        var result = await _httpClient.GetFromJsonAsync<Result<IEnumerable<Post>>>(endpoint);
        
        var posts = result!.Data!.ToList();
        logger.LogInformation("Total posts: {TotalPosts}", posts.Count);

        RecentPost = posts.FirstOrDefault();
        LatestPosts = posts.Skip(1).ToArray();
    }
}