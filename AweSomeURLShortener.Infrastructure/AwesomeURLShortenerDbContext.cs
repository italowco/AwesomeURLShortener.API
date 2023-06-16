using AweSomeURLShortener.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace AweSomeURLShortener.Infrastructure
{
    public class AwesomeURLShortenerDbContext : DbContext
    {
        public AwesomeURLShortenerDbContext(DbContextOptions<AwesomeURLShortenerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UrlRegistryEntity>()
                .Property(e => e.Id)
                .HasDefaultValueSql("NEXT VALUE FOR urlregistry_id_seq");

            
        }

        public DbSet<UrlRegistryEntity> UrlRegistries { get; set; }
    }
}
