using Microsoft.AspNetCore.Mvc;
using StoreProject.BLL.Dtos.Product;
using StoreProject.BLL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto newProduct)
        {
            var createdProductDto = await _productService.AddProduct(newProduct);
            return Created("", createdProductDto);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> PutProduct(int id, ProductDto product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            await _productService.UpdateProduct(product);
            return NoContent();
        }
        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }
        // PUT: api/Products/productId/Users/userId
        [HttpPut("{productId}/userId")]
        public async Task<IActionResult> AddUser(int productId, int userId)
        {
            await _productService.AddUser(productId, userId);
            return NoContent();

        }
    }
}
