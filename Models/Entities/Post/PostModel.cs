using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace student_platform.Models.Entities.Post
{
  public class Post
  {
    public int PostId { get; set; }
    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string Title { get; set; }

    [StringLength(500, ErrorMessage = "Text must be between 3 and 500 characters long", MinimumLength = 3)]
    public string Text { get; set; }


    [DataType(DataType.Date)]
    public DateTime Created { get; set; }

    public PostStatus Status { get; set; }

    public string UserId { get; set; }

    #nullable enable
    public IdentityUser? User { get; set; }

    public List<Comment>? Comments { get; set; }
  }
}