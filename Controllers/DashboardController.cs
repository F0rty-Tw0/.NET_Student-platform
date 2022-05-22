using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using student_platform.Models;

namespace student_platform.Controllers;

public class DashboardController : Controller
{
  private readonly ILogger<DashboardController> _logger;

  public DashboardController(ILogger<DashboardController> logger)
  {
    _logger = logger;
  }

  public IActionResult OnGet()
  {
    return RedirectToPage("Login");
  }

  public IActionResult Index()
  {
    TempData["Route"] = "Dashboard";
    return View();
  }

  public IActionResult Privacy()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
  }
}
