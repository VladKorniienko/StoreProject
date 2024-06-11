using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreProject.BLL.Dtos.Genre;
using StoreProject.BLL.Dtos.Product;
using StoreProject.BLL.Interfaces;
using StoreProject.BLL.Services;
using StoreProject.Common.Constants;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 1000) pageSize = 10; // Adjust max page size as necessary

            var products = await _productService.GetProducts(pageNumber, pageSize);

            var totalItems = await _productService.GetTotalProducts();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            Response.Headers.Append("Total-Count", totalItems.ToString());
            Response.Headers.Append("Total-Pages", totalPages.ToString());
            Response.Headers.Append("Current-Page", pageNumber.ToString());
            Response.Headers.Append("Page-Size", pageSize.ToString());

            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(string id)
        {
            var product = await _productService.GetProduct(id);
            return Ok(product);
        }

        [HttpPost]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<ProductDto>> PostProduct([FromForm]ProductCreateOrUpdateDto newProduct)
        {
            var createdProductDto = await _productService.AddProduct(newProduct);
            return Created("", createdProductDto);
        }

        [HttpPut("{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> PutProduct(string id, [FromForm]ProductCreateOrUpdateDto product)
        {
            await _productService.UpdateProduct(product, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
