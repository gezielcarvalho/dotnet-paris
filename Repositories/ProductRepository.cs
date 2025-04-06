using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetParis.Data;
using DotNetParis.Models;

namespace DotNetParis.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get by Id, returns any subclass of Product
        public async Task<Product> GetByIdAsync(int id)
        {
            // Simulating asynchronous call
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            return await Task.FromResult(product); // Return any Product (including subtypes)
        }

        // Get all products, returns any subclass of Product
        public async Task<List<Product>> GetAllAsync()
        {
            // Simulating asynchronous call
            return await Task.FromResult(_context.Products.ToList());
        }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
            }
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                _context.Products.Remove(existingProduct);
            }
            await Task.CompletedTask;
        }
    }
}

