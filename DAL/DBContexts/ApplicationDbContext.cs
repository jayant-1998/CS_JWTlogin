using JWTEmployeeLoginPortal.DAL.Entities;
using JWTLogin.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWTLogin.DAL.DBContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Registration> registrations { get; set; }
        public DbSet<Secret> secretKey { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>();
            modelBuilder.Entity<Secret>();

        }


    }
}
