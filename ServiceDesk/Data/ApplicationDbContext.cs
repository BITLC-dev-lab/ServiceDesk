using Microsoft.EntityFrameworkCore;
using ServiceDesk.Models;

namespace ServiceDesk.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Category> Categories { get; set; }


    }
}
