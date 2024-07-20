namespace MyBlog.Models;

public record Post(
    int Id,
    string Title,
    string Content,
    Category Category,
    Tag[] Tags,
    Comment[] Comments,
    DateTime CreatedAt);