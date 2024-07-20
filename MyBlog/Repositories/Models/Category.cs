using System.ComponentModel.DataAnnotations;

namespace MyBlog.Repositories.Models;

public class Category
{
    public int CategoryID { get; set; }

    [Required]
    [MinLength(5), MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Post> Posts { get; set; } = [];

    public MyBlog.Models.Category ToModel()
        => new(CategoryID, Name);
}