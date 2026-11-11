
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
        public DbSet<Employer> Employees { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Die Beziehung zwischen Kategorie und Tickets (DeleteBehavior.Restrict verhindert das Löschen einer Kategorie, solange noch Tickets damit verknüpft sind)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Category)
                .WithMany()
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Die Beziehung zwischen Mitarbeiter und Tickets (DeleteBehavior.SetNull setzt das Ticket auf "nicht zugewiesen", falls der Mitarbeiter gelöscht wird)
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Employer)
                .WithMany()
                .HasForeignKey(t => t.Employee_ID)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}