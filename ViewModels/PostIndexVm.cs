namespace student_platform.Models.Entities.Post;
public class PostIndexVm
{
  public IEnumerable<Post> Posts { get; set; }
  public string SearchString { get; set; }
}