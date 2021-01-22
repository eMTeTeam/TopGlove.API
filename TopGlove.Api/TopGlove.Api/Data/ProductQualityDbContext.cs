using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopGlove.Api.Model;

namespace TopGlove.Api.Data
{
    public class ProductQualityDbContext : DbContext
    {
        public ProductQualityDbContext(DbContextOptions<ProductQualityDbContext> options) : base(options) { }
        public DbSet<ProductQuality> ProductQualities { get; set; }
    }
}
