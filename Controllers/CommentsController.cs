using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using student_platform.Models.Entities.Post;
using student_platform.Data;

namespace student_platform.Controllers
{
  public class CommentsController : Controller
  {
    private readonly StudentsDBContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    public CommentsController(StudentsDBContext context, UserManager<IdentityUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    // GET: Comments
    public async Task<IActionResult> Index()
    {
      var studentContext = _context.Comments.Include(c => c.Post);
      TempData["Route"] = "Chats";
      return View(await studentContext.ToListAsync());
    }

    // GET: Comments/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var comment = await _context.Comments
          .Include(c => c.Post)
          .FirstOrDefaultAsync(m => m.CommentId == id);
      if (comment == null)
      {
        return NotFound();
      }
      TempData["Route"] = "Chats";
      return View(comment);
    }

    // GET: Comments/Create
    public IActionResult Create()
    {
      ViewData["PostId"] = new SelectList(_context.Set<Post>(), "PostId", "Title");
      TempData["Route"] = "Chats";
      return View();
    }

    // POST: Comments/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CommentId,Text,Likes,PostId,Created")] Comment comment)
    {
      if (ModelState.IsValid)
      {
        IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
        comment.UserId = user.Id;
        _context.Add(comment);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["PostId"] = new SelectList(_context.Set<Post>(), "PostId", "Title", comment.PostId);
      TempData["Route"] = "Chats";
      return View(comment);
    }

    // GET: Comments/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var comment = await _context.Comments.FindAsync(id);
      if (comment == null)
      {
        return NotFound();
      }
      ViewData["PostId"] = new SelectList(_context.Set<Post>(), "PostId", "Title", comment.PostId);
      TempData["Route"] = "Chats";
      return View(comment);
    }

    // POST: Comments/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("CommentId,Text,Likes,PostId,Created,UserId")] Comment comment)
    {
      if (id != comment.CommentId)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(comment);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!CommentExists(comment.CommentId))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      ViewData["PostId"] = new SelectList(_context.Set<Post>(), "PostId", "Title", comment.PostId);
      TempData["Route"] = "Chats";
      return View(comment);
    }

    // GET: Comments/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var comment = await _context.Comments
          .Include(c => c.Post)
          .FirstOrDefaultAsync(m => m.CommentId == id);
      if (comment == null)
      {
        return NotFound();
      }
      TempData["Route"] = "Chats";
      return View(comment);
    }

    // POST: Comments/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var comment = await _context.Comments.FindAsync(id);
      _context.Comments.Remove(comment);
      await _context.SaveChangesAsync();
      TempData["Route"] = "Chats";
      return RedirectToAction(nameof(Index));
    }

    private bool CommentExists(int id)
    {
      return _context.Comments.Any(e => e.CommentId == id);
    }
  }
}
