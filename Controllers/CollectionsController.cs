using Microsoft.AspNetCore.Mvc;

namespace student_platform.Controllers;

public class CollectionsController : Controller
{
  private readonly ILogger<CollectionsController> _logger;

  public CollectionsController(ILogger<CollectionsController> logger)
  {
    _logger = logger;
  }

  public IActionResult Index()
  {
    TempData["Route"] = "Collections";
    return View();
  }
}