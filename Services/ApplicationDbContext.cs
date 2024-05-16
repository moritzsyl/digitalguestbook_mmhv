using digitalguestbook.Models;
using Microsoft.EntityFrameworkCore;

namespace digitalguestbook.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Appointment> Appointments { get; set; }
    }
}