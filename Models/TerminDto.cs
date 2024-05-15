using System.ComponentModel.DataAnnotations;

namespace digitalguestbook.Models;

public class TerminDto
{
    [Required, MaxLength(100)]
    public String Email { get; set; } = "";
    [Required,MaxLength(100)]
    public String Company { get; set; } = "";
    
    [Required]
    public DateTime Date { get; set; } = DateTime.Now; 
    
    public String Description { get; set; } = "";
}