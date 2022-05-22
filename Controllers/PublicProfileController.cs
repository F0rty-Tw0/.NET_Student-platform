using Microsoft.AspNetCore.Mvc;

namespace student_platform.Controllers;

public class PublicProfileController : Controller
{
  private readonly ILogger<PublicProfileController> _logger;

  public PublicProfileController(ILogger<PublicProfileController> logger)
  {
    _logger = logger;
  }

  public IActionResult Index()
  {
    TempData["Route"] = "PublicProfile";
    return View();
  }
}