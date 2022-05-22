using Microsoft.AspNetCore.Mvc;

namespace student_platform.Controllers;

public class EventsController : Controller
{
  private readonly ILogger<EventsController> _logger;

  public EventsController(ILogger<EventsController> logger)
  {
    _logger = logger;
  }

  public IActionResult Index()
  {
    TempData["Route"] = "Events";
    return View();
  }
}