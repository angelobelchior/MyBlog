namespace MyBlog.Shared.Models;

/// <summary>
/// Result model
/// </summary>
/// <typeparam name="T">Type of Data</typeparam>
public class Result<T>
    where T : class
{
    /// <summary>
    /// Data
    /// </summary>
    public T? Data { get; set; }
    
    /// <summary>
    /// Pagination structure
    /// </summary>
    public Pagination? Pagination { get; set; }
}

/// <summary>
/// Pagination structure
/// </summary>
public class Pagination
{
    /// <summary>
    /// Page Index
    /// </summary>
    public int PageIndex { get; set; }
    /// <summary>
    /// Page Size
    /// </summary>
    public int PageSize { get; set; }
    /// <summary>
    /// Total Pages
    /// </summary>
    public int TotalPages { get; set; }
    /// <summary>
    /// Total Items
    /// </summary>
    public int TotalItems { get; set; }
}