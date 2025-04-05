using System.Collections.Generic;
using DotNetParis.Models;

namespace DotNetParis.Data
{
    public class ApplicationDbContext // Remove inheritance from DbContext
    {
        public List<Product> Products { get; set; } = new() // Use a simple in-memory list
        {
            new Product(1, "Public Product 1"),
            new Product(2, "Private Product 1"),
            new Product(3, "Public Product 2"),
            new Product(4, "Private Product 2")
        };
    }
}
