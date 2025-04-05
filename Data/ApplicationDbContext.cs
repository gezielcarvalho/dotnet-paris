using System.Collections.Generic;

namespace DotNetParis.Data
{
    public class ApplicationDbContext
    {
        public List<Product> Products { get; set; } = new()
        {
            new Product(1, "Public Product 1"),
            new Product(2, "Private Product 1"),
            new Product(3, "Public Product 2"),
            new Product(4, "Private Product 2")
        };
    }
}
