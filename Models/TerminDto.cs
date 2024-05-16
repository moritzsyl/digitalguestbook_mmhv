using System;
using System.ComponentModel.DataAnnotations;

namespace digitalguestbook.Models
{
    public class TerminDto
    {
        [Required, MaxLength(100)]
        public string Email { get; set; } = "";
        
        [Required, MaxLength(100)]
        public string Company { get; set; } = "";
        
        [Required]
        public DateTime Date { get; set; }
        
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }
        
        public string Description { get; set; } = "";
    }
}