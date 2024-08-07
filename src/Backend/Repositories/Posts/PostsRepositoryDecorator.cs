using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using MyBlog.Backend.Counters;
using MyBlog.Shared.Models;
using Post = MyBlog.Shared.Models.Post;

namespace MyBlog.Backend.Repositories.Posts;

internal class PostsRepositoryDecorator(
    ILogger<PostsRepository> logger,
    MyBlogCounters counters,
    IDistributedCache cache,
    PostsRepository repository) : IPostsRepository
{
    private static readonly DistributedCacheEntryOptions DistributedCacheEntryOptions = new DistributedCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromSeconds(30))    //Apenas para efeito de testes
        .SetAbsoluteExpiration(TimeSpan.FromSeconds(30));  //Não use em produção
    
    public async Task<Result<IReadOnlyCollection<Post>>> ListAllAsync(
        CancellationToken cancellationToken, 
        int pageIndex = 1, 
        int pageSize = 3, 
        int? categoryId = null, 
        int? tagId = null)
    {
        if(categoryId is not null)
            counters.IncrementFilter("category", categoryId.Value);
        
        if(tagId is not null)
            counters.IncrementFilter("tag", tagId.Value);
        
        var cacheKey = $"posts_{pageIndex}_{categoryId}_{tagId}";
        var allPosts = await GetOrSetCache(nameof(ListAllAsync), cacheKey,
            () => repository.ListAllAsync(cancellationToken, pageIndex, pageSize, categoryId, tagId),
            cancellationToken);
        return allPosts ?? new Result<IReadOnlyCollection<Post>>();        
    }

    public async Task<Result<IReadOnlyCollection<Post>>> ListNewestAsync(CancellationToken cancellationToken, int maxCount = 5)
    {
        var cacheKey = $"newest_posts_{maxCount}";
        var newestPosts = await GetOrSetCache(nameof(ListNewestAsync), cacheKey, () => repository.ListNewestAsync(cancellationToken, maxCount), cancellationToken);
        return newestPosts ?? new Result<IReadOnlyCollection<Post>>();
    }

    public async Task<Result<Post>> GetByIdAsync(CancellationToken cancellationToken, int id)
    {
        var cacheKey = $"post_{id}";
        var post = await GetOrSetCache(nameof(GetByIdAsync), cacheKey, () => repository.GetByIdAsync(cancellationToken, id), cancellationToken);
        return post ?? new Result<Post>();
    }
    
    private async Task<T?> GetOrSetCache<T>(
        string methodName,
        string key, 
        Func<Task<T>> factory, 
        CancellationToken cancellationToken)
    {
        var json = await cache.GetStringAsync(key, cancellationToken);
        if (json is not null)
        {
            logger.LogInformation("Cache hit for key {Key}", key);
            counters.IncrementCache("cache", methodName);
            return JsonSerializer.Deserialize<T>(json);
        }
        
        logger.LogInformation("Cache miss for key {Key}", key);

        var value = await factory();
        counters.IncrementCache("database", methodName);
        json = JsonSerializer.Serialize(value);
        await cache.SetStringAsync(key, json, DistributedCacheEntryOptions, cancellationToken);
        return value;
    }
}