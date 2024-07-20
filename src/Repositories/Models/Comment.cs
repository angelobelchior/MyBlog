using System.ComponentModel.DataAnnotations;

namespace MyBlog.Repositories.Models;

public class Comment
{
    public int CommentID { get; set; }
    public int PostID { get; set; }
    
    [Required]
    [MinLength(3), MaxLength(100)]
    public string Author { get; set; } = string.Empty;
    
    [Required]
    [MinLength(10), MaxLength(int.MaxValue)]
    public string Content { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public Post Post { get; set; } = default!;
    
    public MyBlog.Models.Comment ToModel()
        => new(CommentID, Author, Content, CreatedAt);
}