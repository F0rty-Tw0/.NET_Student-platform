using Microsoft.AspNetCore.Mvc;

namespace student_platform.Controllers;

public class VolunteersController : Controller
{
  private readonly ILogger<VolunteersController> _logger;

  public VolunteersController(ILogger<VolunteersController> logger)
  {
    _logger = logger;
  }

  public IActionResult Index()
  {
    TempData["Route"] = "Volunteers";
    return View();
  }
}