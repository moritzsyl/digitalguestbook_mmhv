using System.ComponentModel.DataAnnotations;

namespace digitalguestbook.Models;

public class AppointmentCreate
{

    [Required, MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required, MaxLength(100)]
    public string Company { get; set; } = "";

    [Required]
    public DateTime Date { get; set; } = DateTime.Now;

    [MaxLength(250)]
    public string Description { get; set; } = "";
}