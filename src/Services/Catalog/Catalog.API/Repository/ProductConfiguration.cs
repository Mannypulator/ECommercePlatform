using System;
using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.API.Repository;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Description)
            .HasMaxLength(500); // Or leave empty for unlimited text

        // Required for PostgreSQL decimal precision
        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        // Store the List<string> Category as a flattened string or JSONB? 
        // Ideally JSONB for Postgres, but for simplicity let's rely on EF Core defaults or simple conversion if needed.
        // For now, EF Core doesn't auto-map List<string> to a relational table column easily without value converters.
        // Let's use a simple value conversion to store categories as a CSV string, or update to use HasPostgresArray() if sticking strictly to Postgres.
        builder.Property(x => x.Category)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
            .HasMaxLength(500);

        builder.Property(x => x.ImageFile)
            .HasMaxLength(2000);

        // Simplest portable approach for MVP:
        // (If you want true Postgres Arrays, we need the Npgsql specific package features, but let's assume standard behavior first)
    }
}
