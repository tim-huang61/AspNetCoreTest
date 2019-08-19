using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTest201908.Entity
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
                : base(options)
        {
        }

        public DbSet<Profile> Profile { get; set; }
    }
}