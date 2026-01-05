using System;

namespace Catalog.API.Models;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<string> Category { get; set; } = new();
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }

    // Constructor for EF Core
    public Product() { }

    // Factory method for cleaner instantiation
    public static Product Create(Guid id, string name, List<string> category, string description, string imageFile, decimal price)
    {
        // Simple domain logic/validation can go here
        if (price < 0) throw new ArgumentException("Price cannot be negative");

        return new Product
        {
            Id = id,
            Name = name,
            Category = category,
            Description = description,
            ImageFile = imageFile,
            Price = price
        };
    }
}
