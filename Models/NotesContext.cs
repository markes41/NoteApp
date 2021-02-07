using Microsoft.EntityFrameworkCore;

namespace NoteApp.Models
{
    public class NotesContext : DbContext
    {
        public NotesContext(DbContextOptions<NotesContext> options)
            : base (options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}