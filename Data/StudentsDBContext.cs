using Microsoft.EntityFrameworkCore;
using student_platform.Models.Entities.Post;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace student_platform.Data
{
  public class StudentsDBContext : IdentityDbContext
  {
    public StudentsDBContext(DbContextOptions<StudentsDBContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      this.SeedPosts(builder);
      this.UsersSeed(builder);
      this.SeedPostComments(builder);
    }

    private void UsersSeed(ModelBuilder builder)
    {
      var user1 = new IdentityUser
      {
        Id = "1",
        Email = "art@art.art",
        EmailConfirmed = true,
        UserName = "art@art.art",
        NormalizedUserName = "ART@ART.ART"
      };

      var user2 = new IdentityUser
      {
        Id = "2",
        Email = "test@kea.dk",
        EmailConfirmed = true,
        UserName = "test@kea.dk",
        NormalizedUserName = "TEST@KEA.DK"
      };

      PasswordHasher<IdentityUser> passHash = new PasswordHasher<IdentityUser>();
      user1.PasswordHash = passHash.HashPassword(user1, "aA123456!");
      user2.PasswordHash = passHash.HashPassword(user2, "aA123456!");

      builder.Entity<IdentityUser>().HasData(
          user1, user2
      );
    }

    private void SeedPosts(ModelBuilder builder)
    {
      Post post = new Post()
      {
        PostId = 1,
        Title = "Post 1",
        Text = "This is post 1",
        UserId = "1",
        // UserId = "bbf360c7-70eb-4676-bcb4-196ad1eff079",
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