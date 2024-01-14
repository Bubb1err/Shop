using CatalogAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatalogAPI.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) 
        {   
        }

        public DbSet<CatalogItem> Items { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
