using Microsoft.EntityFrameworkCore;
using projectNotes.Models;

namespace projectNotes.Database
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options ): base(options)
        {

        }

        // All models in the database:

        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NoteCategory> NoteCategories { get; set; }
        public DbSet<NoteTag> NoteTags { get; set; }



    }
}
