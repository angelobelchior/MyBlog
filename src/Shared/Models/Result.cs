namespace MyBlog.Shared.Models;

public class Result<T>
    where T : class
{
    public T? Data { get; set; }
    public Pagination? Pagination { get; set; }
}

public class Pagination
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
}