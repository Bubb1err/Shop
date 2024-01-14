using BasketAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BasketAPI.Data
{
  public class BasketDbContext : DbContext
  {
        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base (options)
        {
            
        }
    public DbSet<BasketItem> BasketItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

      base.OnModelCreating(modelBuilder);
    }
  }
}
