using digitalguestbook.Models;
using digitalguestbook.Services;
using Microsoft.AspNetCore.Mvc;

namespace digitalguestbook.Controllers
{
    public class TerminController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment environment;

        public TerminController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            var termine = context.Termine.OrderByDescending(t => t.Id).ToList();
            return View(termine);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TerminDto terminDto)
        {
            if (!ModelState.IsValid)
            {
                return View(terminDto);
            }
            
            // Neuen Termin in die Datenbank hinzufügen
            Termine termine = new Termine()
            {
                Email = terminDto.Email,
                Company = terminDto.Company,
                Description = terminDto.Description,
                Date = terminDto.Date,
                Time = terminDto.Time
            };

            context.Termine.Add(termine);
            context.SaveChanges();

            return RedirectToAction("Index", "Termin");
        }

        public IActionResult Delete(int id)
        {
            var termin = context.Termine.Find(id);
            if (termin == null)
            {
                return RedirectToAction("Index", "Termin");
            }

            context.Termine.Remove(termin);
            context.SaveChanges(true);
            return RedirectToAction("Index", "Termin");
        }
    }
}