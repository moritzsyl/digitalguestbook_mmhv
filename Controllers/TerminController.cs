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
        var termine = context.Termine.ToList();
        return View(termine);
    }
}