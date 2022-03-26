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

    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
  }
}