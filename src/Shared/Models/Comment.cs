namespace MyBlog.Shared.Models;

public record Comment(
    int Id,
    string Author,
    string Content,
    DateTime CreatedAt);