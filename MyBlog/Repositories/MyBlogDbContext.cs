using Microsoft.EntityFrameworkCore;
using MyBlog.Repositories.Models;

namespace MyBlog.Repositories;

public class MyBlogDbContext(DbContextOptions<MyBlogDbContext> options)
    : DbContext(options)
{

    public DbSet<Category> Categories { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<PostTag> PostTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>()
            .HasKey(c => c.CategoryID);

        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Post>()
            .HasKey(p => p.PostID);

        modelBuilder.Entity<Post>()
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(255);

        modelBuilder.Entity<Post>()
            .Property(p => p.Content)
            .IsRequired();

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CategoryID);

        modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostID);

        modelBuilder.Entity<Post>()
            .HasMany(p => p.Tags)
            .WithOne(pt => pt.Post)
            .HasForeignKey(pt => pt.PostID);

        modelBuilder.Entity<Tag>()
            .HasKey(t => t.TagID);

        modelBuilder.Entity<Tag>()
            .Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Tag>()
            .HasMany(t => t.Posts)
            .WithOne(pt => pt.Tag)
            .HasForeignKey(pt => pt.TagID);

        modelBuilder.Entity<PostTag>()
            .HasKey(pt => new { pt.PostID, pt.TagID });

        modelBuilder.Entity<Comment>()
            .HasKey(c => c.CommentID);

        modelBuilder.Entity<Comment>()
            .Property(c => c.Author)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Comment>()
            .Property(c => c.Content)
            .IsRequired();
    }
    
    public IQueryable<Post> GetPostQueryable() => Posts
        .Include(p => p.Category)
        .Include(p => p.Tags).ThenInclude(t => t.Tag)
        .Include(p => p.Comments)
        .AsNoTrackingWithIdentityResolution();
}
