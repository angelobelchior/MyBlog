using System.ComponentModel.DataAnnotations;

namespace MyBlog.Repositories.Models;

public class Tag
{
    public int TagID { get; set; }
    
    [Required]
    [MinLength(3), MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    public ICollection<PostTag> Posts { get; set; } = [];
    
    public MyBlog.Models.Tag ToModel()
        => new(TagID, Name);
}