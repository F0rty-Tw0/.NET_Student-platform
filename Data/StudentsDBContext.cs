using Microsoft.EntityFrameworkCore;
using student_platform.Models.Entities.Post;

namespace student_platform.Data
{
  public class StudentsDBContext : DbContext
  {
    public StudentsDBContext(DbContextOptions<StudentsDBContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      this.SeedPosts(builder);
      this.SeedPostComments(builder);
    }

    private void SeedPosts(ModelBuilder builder)
    {
      Post post = new Post()
      {
        PostId = 1,
        Title = "Post 1",
        Text = "This is post 1",
        Created = DateTime.Now
      };
      builder.Entity<Post>().HasData(post);
    }

    private void SeedPostComments(ModelBuilder builder)
    {
      builder.Entity<Comment>().HasData(
          new Comment()
          {
            CommentId = 1,
            PostId = 1,
            Text = "This is comment 1",
            Created = DateTime.Now
          }
          );
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
  }
}