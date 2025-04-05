using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetParis.Data;

namespace DotNetParis.Models
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async ValueTask<Product> GetByIdAsync(int productId)
        {
            await Task.Delay(100); // Simulate async operation
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }

        public async ValueTask<IEnumerable<Product>> GetAllAsync()
        {
            await Task.Delay(100); // Simulate async operation
            return _context.Products;
        }

        public async ValueTask AddAsync(Product product)
        {
            await Task.Delay(100); // Simulate async operation
            _context.Products.Add(product);
        }

        public async ValueTask UpdateAsync(Product product)
        {
            await Task.Delay(100); // Simulate async operation
            var existingProduct = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
            }
        }

        public async ValueTask RemoveAsync(Product product)
        {
            await Task.Delay(100); // Simulate async operation
            _context.Products.Remove(product);
        }
    }
}
