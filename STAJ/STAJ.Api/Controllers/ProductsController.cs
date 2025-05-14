using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using STAJ.Api.Data; // STAJ.Api.Data.Product kullanılıyor
// using STAJ.Frontend.Models; // Eğer gerek yoksa bu satırı kaldırabilirsiniz

namespace STAJ.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // GET: api/Products/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { Message = $"Product with ID {id} not found." });

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] STAJ.Api.Data.Product product)
        {
            if (product == null)
                return BadRequest(new { Message = "Invalid product data." });

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/Products/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] STAJ.Api.Data.Product product)
        {
            if (id != product.Id)
                return BadRequest(new { Message = "Product ID mismatch." });

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
                return NotFound(new { Message = $"Product with ID {id} not found." });

            // Update product properties
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Products/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound(new { Message = $"Product with ID {id} not found." });

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Product with ID {id} has been deleted." });
        }
    }
}
