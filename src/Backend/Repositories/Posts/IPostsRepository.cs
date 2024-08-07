using MyBlog.Shared.Models;
using Post = MyBlog.Shared.Models.Post;

namespace MyBlog.Backend.Repositories.Posts;

public interface IPostsRepository
{
    Task<Result<IReadOnlyCollection<Post>>> ListAllAsync(CancellationToken cancellationToken,
        int pageIndex = 1,
        int pageSize = 3,
        int? categoryId = null,
        int? tagId = null);
    
    Task<Result<IReadOnlyCollection<Post>>> ListNewestAsync(CancellationToken cancellationToken,
        int maxCount = 5);
    
    Task<Result<Post>> GetByIdAsync(CancellationToken cancellationToken,
        int id);
}