using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetParis.Models;
using DotNetParis.Services;

namespace DotNetParis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductController(ProductService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetOneProduct(int id)
        {
            var product = await _service.GetOneProductAsync(id);
            if (product == null) return NotFound();
            return Ok(product);  // Can return any subclass of Product (PublicProduct or PrivateProduct)
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var products = await _service.GetAllProductsAsync();
            return Ok(products);  // Can return a mix of PublicProduct and PrivateProduct
        }

        // New endpoint to get all PublicProducts
        [HttpGet("public")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllPublicProducts()
        {
            var products = await _service.GetAllPublicProductsAsync();
            return Ok(products);
        }

        // New endpoint to get all PrivateProducts
        [HttpGet("private")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllPrivateProducts()
        {
            var products = await _service.GetAllPrivateProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody] Product product)
        {
            await _service.CreateAsync(product);
            return CreatedAtAction(nameof(GetOneProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id) return BadRequest();
            await _service.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _service.GetOneProductAsync(id);
            if (product == null) return NotFound();
            await _service.DeleteAsync(product);
            return NoContent();
        }
    }
}

