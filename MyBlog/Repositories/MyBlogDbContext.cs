using Microsoft.EntityFrameworkCore;

namespace MyBlog.Repositories;

public class MyBlogDbContext(DbContextOptions<MyBlogDbContext> options)
    : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
