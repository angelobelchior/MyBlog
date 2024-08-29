namespace MyBlog.Shared.Models;

/// <summary>
/// Category model
/// </summary>
/// <param name="Id">Category Id</param>
/// <param name="Name">Categoty Name</param>
public record Category(int Id, string Name);