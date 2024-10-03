using Microsoft.AspNetCore.Mvc;
using InertiaCore;

namespace IntertiaDotnetStarter.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return Inertia.Render("Index", new { Name = "World" });
    }

    [HttpGet("/about")]
    public IActionResult About()
    {
        return View("About");
    }

    [HttpGet("/counter")]
    public IActionResult Counter()
    {
        return Inertia.Render("Counter");
    }
}