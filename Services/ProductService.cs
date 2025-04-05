using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetParis.Models;
using DotNetParis.Repositories;

namespace DotNetParis.Services
{
    public class ProductService
    {
        private readonly ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<Product> GetOnePublicProductAsync(int productId)
        {
            var product = await _repository.GetByIdAsync(productId);
            return product != null && product.Name.Contains("Public") ? product : null;
        }

        public async ValueTask<Product> GetOnePrivateProductAsync(int productId)
        {
            var product = await _repository.GetByIdAsync(productId);
            return product != null && product.Name.Contains("Private") ? product : null;
        }

        public async ValueTask<IEnumerable<Product>> GetAllPublicProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Where(p => p.Name.Contains("Public"));
        }

        public async ValueTask<IEnumerable<Product>> GetAllPrivateProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Where(p => p.Name.Contains("Private"));
        }

        public async ValueTask CreateAsync(Product product)
        {
            await _repository.AddAsync(product);
        }

        public async ValueTask UpdateAsync(Product product)
        {
            await _repository.UpdateAsync(product);
        }

        public async ValueTask DeleteAsync(Product product)
        {
            await _repository.RemoveAsync(product);
        }
    }
}
