using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace student_platform.Models.Entities.Post
{
  public class Comment
  {
    public int CommentId { get; set; }

    [StringLength(500, ErrorMessage = "Text must be between 3 and 500 characters long", MinimumLength = 3)]
    [Required]
    public string Text { get; set; }
    public int Likes { get; set; }

    [DataType(DataType.Date)]
    public DateTime Created { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public string UserId { get; set; }
    public IdentityUser? User { get; set; }
  }
}