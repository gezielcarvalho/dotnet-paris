using System.Collections.Generic;
using System.Linq;
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

        public async ValueTask<Product> GetOnePublicProductAsync(int productId)
        {
            await Task.Delay(100); // Simulate async operation
            return _context.Products.FirstOrDefault(p => p.Id == productId && p.Name.Contains("Public"));
        }

        public async ValueTask<Product> GetOnePrivateProductAsync(int productId)
        {
            await Task.Delay(100); // Simulate async operation
            return _context.Products.FirstOrDefault(p => p.Id == productId && p.Name.Contains("Private"));
        }

        public async ValueTask<IEnumerable<Product>> GetAllPublicProductAsync()
        {
            await Task.Delay(100); // Simulate async operation
            return _context.Products.Where(p => p.Name.Contains("Public"));
        }

        public async ValueTask<IEnumerable<Product>> GetAllPrivateProductAsync()
        {
            await Task.Delay(100); // Simulate async operation
            return _context.Products.Where(p => p.Name.Contains("Private"));
        }

        public async ValueTask CreateAsync(Product product)
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

        public async ValueTask DeleteAsync(Product product)
        {
            await Task.Delay(100); // Simulate async operation
            _context.Products.Remove(product);
        }
    }
}
