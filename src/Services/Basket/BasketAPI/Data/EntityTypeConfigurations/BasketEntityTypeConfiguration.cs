using BasketAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketAPI.Data.EntityTypeConfigurations
{
  public class BasketEntityTypeConfiguration : IEntityTypeConfiguration<BasketItem>
  {
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
      builder.HasKey(item => item.Id);

      builder.Property(item => item.UserId).IsRequired();

      builder.Property(item => item.CatalogItemId).IsRequired();

      builder.Property(item => item.ItemName).IsRequired();

      builder.Property(item => item.ImageUrl).IsRequired();

      foreach (var property in builder.Metadata.GetProperties()
            .Where(p => p.ClrType == typeof(decimal)))
      {
        property.SetPrecision(18);
        property.SetScale(2);   
      }
    }
  }
}
