using Microsoft.AspNetCore.Mvc;
using student_platform.Data;
using student_platform.Models.Entities.Post;

namespace student_platform.Controllers;

public class PostsController : Controller
{
  private StudentsDBContext _context;
  public PostsController(StudentsDBContext context)
  {
    this._context = context;
  }
  public IActionResult Index()
  {
    return View(_context.Posts.ToList());
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
  public IActionResult AddPost([Bind("Title,Text")] Post post)
  {
    if (ModelState.IsValid)
    {
      post.Created = DateTime.Now;
      _context.Posts.Add(post);
      _context.SaveChanges();
      // save it to db
    }
    Console.WriteLine(post.Title);
    return RedirectToAction("Post");
  }

  public IActionResult EditPost(int id)
  {
    Post p = _context.Posts.Find(id);
    return View(p);
  }

  [HttpPost]
  public IActionResult EditPost(int id, [Bind("Id", "Title", "Text")] Post post)
  {
    if (ModelState.IsValid)
    {
      _context.Posts.Update(post);
      _context.SaveChanges();
      return RedirectToAction("Index");
    }

    return View();
  }
}
