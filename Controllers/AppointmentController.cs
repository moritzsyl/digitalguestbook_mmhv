using digitalguestbook.Models;
using digitalguestbook.Services;
using Microsoft.AspNetCore.Mvc;

namespace digitalguestbook.Controllers;

public class AppointmentController : Controller
{
    private readonly ApplicationDbContext context;

    public AppointmentController(ApplicationDbContext context)
    {
        this.context = context;
    }
    public IActionResult Index()
    {
        var appointments = context.Appointments.OrderByDescending(a  => a.Id).ToList();
        return View(appointments);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(AppointmentCreate appointmentCreate)
    {
        if (!ModelState.IsValid)
        {
            return View(appointmentCreate);
        }

        //Neuen Termin in die Datenbank hinzufügen
        Appointment appointment = new Appointment()
        {
            Email = appointmentCreate.Email,
            Company = appointmentCreate.Company,
            Description = appointmentCreate.Description,
            Date = appointmentCreate.Date,
            Time = appointmentCreate.Time
            
        };

        context.Appointments.Add(appointment);
        context.SaveChanges();
        
        return RedirectToAction("Index", "Appointment");
    }

    public IActionResult Delete(int id)
    {
        var appointment = context.Appointments.Find(id);
        if (appointment == null)
        {
            return RedirectToAction("Index", "Appointment");
        }

        context.Appointments.Remove(appointment);
        context.SaveChanges(true);
        return RedirectToAction("Index", "Appointment");
    }
}