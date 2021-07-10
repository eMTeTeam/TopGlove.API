using Microsoft.EntityFrameworkCore;
using TopGlove.Api.Model;

namespace TopGlove.Api.Data
{
    public class ProductQualityDbContext : DbContext
    {
        public ProductQualityDbContext(DbContextOptions<ProductQualityDbContext> options) : base(options) { }

        public DbSet<ProductQuality> ProductQualities { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
