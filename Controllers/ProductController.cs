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

        [HttpGet("{id}/public")]
        public async Task<ActionResult<Product>> GetOnePublicProduct(int id)
        {
            var product = await _service.GetOnePublicProductAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("{id}/private")]
        public async Task<ActionResult<Product>> GetOnePrivateProduct(int id)
        {
            var product = await _service.GetOnePrivateProductAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet("public")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllPublicProducts()
        {
            var products = await _service.GetAllPublicProductsAsync();
            return Ok(products);
        }

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
            return CreatedAtAction(nameof(GetOnePublicProduct), new { id = product.Id }, product);
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
            var product = await _service.GetOnePublicProductAsync(id) ?? await _service.GetOnePrivateProductAsync(id);
            if (product == null) return NotFound();
            await _service.DeleteAsync(product);
            return NoContent();
        }
    }
}
