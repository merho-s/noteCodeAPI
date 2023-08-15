using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using noteCodeAPI.Models;

namespace noteCodeAPI.Tools
{
    public class DataDbContext : DbContext
    {
        public DbSet<UserApp> Users { get; set; }
        public DbSet<Note> Notes { get;set; }
        public DbSet<Codetag> Codetags { get; set; }
        public DbSet<UnusedActiveToken> UnusedActiveTokens { get; set; }

        protected readonly IConfiguration _configuration;

        public DataDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_configuration.GetValue<string>("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("ProductionDB"));
            }
            else
            {
                optionsBuilder.UseSqlite(_configuration.GetConnectionString("DevelopmentDB"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>()
                .HasMany(n => n.Codetags)
                .WithMany(t => t.Notes)
                .UsingEntity(j =>
                {
                    j.ToTable("notes_tags");
                    j.Property("CodetagsId").HasColumnName("tag_id");
                    j.Property("NotesId").HasColumnName("note_id");
                });
        }
    }
}
