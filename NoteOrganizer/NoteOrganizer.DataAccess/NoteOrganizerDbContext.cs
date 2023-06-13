using Microsoft.EntityFrameworkCore;
using NoteOrganizer.Model;

namespace NoteOrganizer.DataAccess
{
    public class NoteOrganizerDbContext : DbContext
    {
        public NoteOrganizerDbContext(DbContextOptions<NoteOrganizerDbContext>option) : base(option) 
        {
                
        }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Note>()
                .HasOne<User>()
                .WithMany(g => g.Notes)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
