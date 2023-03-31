using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ItemLog.Models;
using ItemLog.Context;

namespace ItemLog.Controllers;

public class HomeController : Controller
{
    private readonly DataContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(DataContext context ,ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.Categories = _context.Categories.ToList();
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

