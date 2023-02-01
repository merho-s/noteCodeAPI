using Microsoft.EntityFrameworkCore;
using noteCodeAPI.Models;

namespace noteCodeAPI.Tools
{
    public class DataDbContext : DbContext
    {
        public DbSet<UserApp> Users { get; set; }
        public DbSet<Note> Notes { get;set; }
        public DbSet<Codetag> Codetags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=noteCodeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
