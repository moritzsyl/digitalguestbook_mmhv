using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace digitalguestbook.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required, MaxLength(100)]
        [DataType(DataType.Text)]
        public string Company { get; set; } = "";

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(250)]
        [DataType(DataType.Text)]
        public string Description { get; set; } = "";
    }
}