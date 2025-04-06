using System.Collections.Generic;
using DotNetParis.Models;

namespace DotNetParis.Data
{
    public class ApplicationDbContext
    {
        public List<Product> Products { get; set; } = new()
        {
            new PublicProduct(1, "Public Product 1"),
            new PrivateProduct(2, "Private Product 1"),
            new PublicProduct(3, "Public Product 2"),
            new PrivateProduct(4, "Private Product 2")
        };
    }
}
