using JWTLogin.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace JWTLogin.DAL.DBContexts
{
    public class JWTLoginDbContext : DbContext
    {
        public JWTLoginDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<RegisteredEntity> registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisteredEntity>();
        }


    }
}
