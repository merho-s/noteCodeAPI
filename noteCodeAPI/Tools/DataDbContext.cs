using Microsoft.EntityFrameworkCore;
using noteCodeAPI.Models;

namespace noteCodeAPI.Tools
{
    public class DataDbContext : DbContext
    {
        public DbSet<UserApp> Users { get; set; }
        public DbSet<Note> Notes { get;set; }
        public DbSet<Codetag> Codetags { get; set; }
        public DbSet<UnusedActiveToken> UnusedActiveTokens { get; set; }
        public DbSet<WaitingUser> WaitingUsers { get; set; }

        public DataDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasMany(n => n.Codetags)
                .WithMany(t => t.Notes)
                .UsingEntity(j => {
                    j.ToTable("notes_tags");
                    j.Property("CodetagsId").HasColumnName("tag_id");
                    j.Property("NotesId").HasColumnName("note_id");
                });
        }
    }
}
