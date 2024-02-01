using Microsoft.AspNetCore.Mvc;
using StoreProject.BLL.Dtos;
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
            var products = _productService.GetProducts();
            return Ok(products);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto newProduct)
        {
            if (!_productService.AddProduct(newProduct, out ProductDto createdProductDto, out string error))
                return BadRequest(error);
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
            if (!_productService.UpdateProduct(product, out string error))
                return NotFound(error);
            return NoContent();
        }
        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!_productService.DeleteProduct(id, out string error))
            {
                return NotFound(error);
            }
            return NoContent();
        }
        // PUT: api/Products/productId/Users/userId
        [HttpPut("{productId}/userId")]
        public async Task<IActionResult> AddUser (int productId, int userId)
        {
            if(!_productService.AddUser(productId,userId, out string error))
            {
                return NotFound(error);
            }
            return NoContent();
        }
    }
}
