using System.ComponentModel.DataAnnotations;

namespace digitalguestbook.Models;

public class Termine
{
    public int Id { get; set; }
    [MaxLength(100)]
    public String Email { get; set; } = "";
    [MaxLength(100)]
    public String Company { get; set; } = "";
    public DateTime Date { get; set; } 
    public String Description { get; set; } = "";
}