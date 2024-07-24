namespace MyBlog.Backend.Repositories.Models;

public class PostTag
{
    public int PostID { get; set; }
    public Post Post { get; set; } = default!;

    public int TagID { get; set; }
    public Tag Tag { get; set; } = default!;
}