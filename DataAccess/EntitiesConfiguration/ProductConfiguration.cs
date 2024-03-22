using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DataAccess.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Set Primary Key
            builder.HasKey(x => x.Id);

            // Set property configuration
            builder.Property(x => x.Name)
                .HasMaxLength(180)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(1024);

            // Set relationship configuration
            builder.HasOne<Category>(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId);
        }
    }
}
