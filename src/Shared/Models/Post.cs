namespace MyBlog.Shared.Models;

/// <summary>
/// Post model
/// </summary>
/// <param name="Id">Post Id</param>
/// <param name="Title">Post Title</param>
/// <param name="Content">Post Content</param>
/// <param name="Category">Post Category</param>
/// <param name="Tags">Post Tags</param>
/// <param name="Comments">Post Comments</param>
/// <param name="CreatedAt">Post CreatedAt</param>
public record Post(
    int Id,
    string Title,
    string Content,
    Category Category,
    Tag[] Tags,
    Comment[] Comments,
    DateTime CreatedAt);