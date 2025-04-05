using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DotNetParis.Models;

namespace DotNetParis.Data
{
    public class ApplicationDbContext : DbContext // Inherit from DbContext
    {
        public DbSet<Product> Products { get; set; } // Change List<Product> to DbSet<Product>

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<Product>().HasData(
                new Product(1, "Public Product 1"),
                new Product(2, "Private Product 1"),
                new Product(3, "Public Product 2"),
                new Product(4, "Private Product 2")
            );
        }
    }
}
