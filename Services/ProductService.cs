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

        // Works for both PublicProduct and PrivateProduct
        public async ValueTask<Product> GetOneProductAsync(int productId)
        {
            var product = await _repository.GetByIdAsync(productId);
            return product;  // Returns any subclass of Product (e.g., PublicProduct or PrivateProduct)
        }

        public async ValueTask<IEnumerable<Product>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products;  // Returns any subclass of Product
        }

               // New method to get only PublicProducts
        public async ValueTask<IEnumerable<Product>> GetAllPublicProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Where(p => p is PublicProduct);  // Filter only PublicProducts
        }

        // New method to get only PrivateProducts
        public async ValueTask<IEnumerable<Product>> GetAllPrivateProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Where(p => p is PrivateProduct);  // Filter only PrivateProducts
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
