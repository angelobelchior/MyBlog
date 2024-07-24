using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OutputCaching;
using MyBlog.Shared.Models;

namespace MyBlog.Frontend.Pages;

public class PostModel(
    IHttpClientFactory httpClientFactory) 
    : PageModel
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("MyBlog.Backend");
    public Post? Post { get; set; }
    
    [OutputCache]
    public async Task OnGet(int id)
    {
        var endpoint = $"/posts/{id}";
        var result = await _httpClient.GetFromJsonAsync<Result<Post>>(endpoint);
        Post = result!.Data;
    }
}