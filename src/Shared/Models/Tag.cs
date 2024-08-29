namespace MyBlog.Shared.Models;

/// <summary>
/// Tag model
/// </summary>
/// <param name="Id">Tag Id</param>
/// <param name="Name">Tag Name</param>
public record Tag(
    int Id,
    string Name);