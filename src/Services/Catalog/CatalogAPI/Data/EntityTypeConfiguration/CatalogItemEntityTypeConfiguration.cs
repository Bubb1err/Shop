using CatalogAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogAPI.Data.EntityTypeConfiguration
{
    public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.HasKey(item => item.Id);

            builder.Property(item => item.Title).IsRequired();

            builder.Property(item => item.Description).IsRequired();

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
