using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetParis.Models
{
    public class ProductRepository
    {
        private readonly List<Product> _products = new()
        {
            new Product(1, "Public Product 1"),
            new Product(2, "Private Product 1"),
            new Product(3, "Public Product 2"),
            new Product(4, "Private Product 2")
        };

        public async ValueTask<Product> GetOnePublicProductAsync(int productId)
        {
            await Task.Delay(100); // Simulate async operation
            return _products.FirstOrDefault(p => p.Id == productId && p.Name.Contains("Public"));
        }

        public async ValueTask<Product> GetOnePrivateProductAsync(int productId)
        {
            await Task.Delay(100); // Simulate async operation
            return _products.FirstOrDefault(p => p.Id == productId && p.Name.Contains("Private"));
        }

        public async ValueTask<IEnumerable<Product>> GetAllPublicProductAsync()
        {
            await Task.Delay(100); // Simulate async operation
            return _products.Where(p => p.Name.Contains("Public"));
        }

        public async ValueTask<IEnumerable<Product>> GetAllPrivateProductAsync()
        {
            await Task.Delay(100); // Simulate async operation
            return _products.Where(p => p.Name.Contains("Private"));
        }

        public async ValueTask CreateAsync(Product product)
        {
            await Task.Delay(100); // Simulate async operation
            _products.Add(product);
        }

        public async ValueTask UpdateAsync(Product product)
        {
            await Task.Delay(100); // Simulate async operation
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
            }
        }

        public async ValueTask DeleteAsync(Product product)
        {
            await Task.Delay(100); // Simulate async operation
            _products.Remove(product);
        }
    }
}
