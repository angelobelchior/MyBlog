using System.ComponentModel.DataAnnotations;

namespace MyBlog.Backend.Repositories.Models;

public class Category
{
    public int CategoryID { get; set; }

    [Required]
    [MinLength(5), MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Post> Posts { get; set; } = [];

    public Shared.Models.Category ToModel()
        => new(CategoryID, Name);
}