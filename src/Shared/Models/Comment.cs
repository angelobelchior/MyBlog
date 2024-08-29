namespace MyBlog.Shared.Models;

/// <summary>
/// Comment model
/// </summary>
/// <param name="Id">Comment Id</param>
/// <param name="Author">Comment Author</param>
/// <param name="Content">Comment Content</param>
/// <param name="CreatedAt">Comment CreatedAt</param>
public record Comment(
    int Id,
    string Author,
    string Content,
    DateTime CreatedAt);