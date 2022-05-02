using Microsoft.AspNetCore.Mvc;
using student_platform.Data;
using student_platform.Models.Entities.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace student_platform.Controllers;
[Authorize]
public class PostsController : Controller
{
  private StudentsDBContext _context;
  private readonly UserManager<IdentityUser> _userManager;
  public PostsController(StudentsDBContext context, UserManager<IdentityUser> userManager)
  {
    _context = context;
    _userManager = userManager;
  }

  [AllowAnonymous]
  public IActionResult Index(string SearchString = "")
  {
    if (SearchString == null)
    {
      SearchString = "";
    }
    var posts = from p in _context.Posts select p;

    posts = posts.Where(x => x.Title.Contains(SearchString) ||
        x.Text.Contains(SearchString)).Include(y => y.User);

    var vm = new PostIndexVm
    {
      Posts = posts.ToList(),
      SearchString = SearchString
    };

    return View(vm);
  }
  public IActionResult Post()
  {
    ViewBag.name = "Art";
    return View();

  }
  public IActionResult AddPost()
  {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> AddPostAsync([Bind("Title,Text")] Post post)
  {
    if (ModelState.IsValid)
    {
      IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
      post.Created = DateTime.Now;
      post.UserId = user.Id;
      _context.Posts.Add(post);
      _context.SaveChanges();
      // save it to db
    }
    Console.WriteLine(post.Title);
    return RedirectToAction("Post");
  }

  public IActionResult EditPost(int id)
  {
    Post p = _context.Posts.Include(x => x.Comments).ThenInclude(x => x.User).First(x => x.PostId == id);
    
    return View(p);
  }

  [HttpPost]
  public IActionResult EditPost(int id, [Bind("PostId", "Title", "Text")] Post post)
  {
    if (ModelState.IsValid)
      Console.WriteLine(post.PostId);
    {
      _context.Posts.Update(post);
      _context.SaveChanges();
      return RedirectToAction("Index");
    }

    return View();
  }
}
