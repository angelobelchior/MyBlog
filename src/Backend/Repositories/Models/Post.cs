using System.ComponentModel.DataAnnotations;

namespace MyBlog.Backend.Repositories.Models;

public class Post
{
    public int PostID { get; set; }
    public int CategoryID { get; set; }

    [Required]
    [MinLength(10), MaxLength(255)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [MinLength(10), MaxLength(int.MaxValue)]
    public string Content { get; set; } = string.Empty;
    
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public Category Category { get; set; } = default!;
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<PostTag> Tags { get; set; } = [];

    public Shared.Models.Post ToModel()
        => new (
            PostID,
            Title,
            Content,
            Category.ToModel(),
            Tags.Select(t => t.Tag.ToModel()).ToArray(),
            Comments.Select(c => c.ToModel()).ToArray(),
            CreatedAt
        );
}