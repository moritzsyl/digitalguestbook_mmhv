using digitalguestbook.Services;
using Microsoft.AspNetCore.Mvc;

namespace digitalguestbook.Controllers;

public class TerminController : Controller
{
    private readonly ApplicationDbContext context;
    // GET
    public TerminController(ApplicationDbContext context)
    {
        this.context = context;
    }
    public IActionResult Index()
    {
        var termine = context.Termine.OrderByDescending(t=>t.Id).ToList();
        return View(termine);
    }

    public IActionResult Create()
    {
        return View();
    }

}