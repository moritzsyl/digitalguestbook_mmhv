using System;
using System.ComponentModel.DataAnnotations;

namespace digitalguestbook.Models
{
    public class Termine
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Email { get; set; } = "";

        [MaxLength(100)]
        public string Company { get; set; } = "";

        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        public string Description { get; set; } = "";
    }
}