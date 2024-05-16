using System.ComponentModel.DataAnnotations;

namespace digitalguestbook.Models;

public class AppointmentCreate
{

    [Required, MaxLength(100)]
    public string Email { get; set; } = "";
        
    [Required, MaxLength(100)]
    public string Company { get; set; } = "";
        
    [Required]
    public DateTime Date { get; set; }
        
    [Required]
    [DataType(DataType.Time)]
    public TimeSpan Time { get; set; }
        
    public string Description { get; set; } = "";
}