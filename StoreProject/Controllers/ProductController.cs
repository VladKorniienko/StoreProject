using Microsoft.AspNetCore.Mvc;
using StoreProject.BLL.Dtos.Product;
using StoreProject.BLL.Interfaces;
using StoreProject.BLL.Services;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.UnitOfWork;

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
            try
            {
                var products = await _productService.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                //logging
                return StatusCode(500, "An unexpected error occurred while retrieving products.");
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto newProduct)
        {
            try
            {
                var createdProductDto = await _productService.AddProduct(newProduct);
                return Created("", createdProductDto);
            }
            catch (ArgumentException ex)
            {
                //logging
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                //logging
                return StatusCode(500, "An unexpected error occurred while adding the product.");
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> PutProduct(int id, ProductDto product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            try
            {
                await _productService.UpdateProduct(product);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                //logging
                return StatusCode(500, "An unexpected error occurred while updating the product.");
            }
        }
        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                //logging
                return StatusCode(500, "An unexpected error occurred while deleting the product.");
            }

        }
        // PUT: api/Products/productId/Users/userId
        [HttpPut("{productId}/userId")]
        public async Task<IActionResult> AddUser(int productId, int userId)
        {
            try
            {
                await _productService.AddUser(productId, userId);
                return NoContent();
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                //logging
                return StatusCode(500, "An unexpected error occurred while adding the product.");
            }

        }
    }
}
