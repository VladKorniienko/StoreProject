using Microsoft.AspNetCore.Mvc;
using StoreProject.BLL.Dtos.Category;
using StoreProject.BLL.Interfaces;

namespace StoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryPartialDto>>> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(string id)
        {
            var category = await _categoryService.GetCategory(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryPartialDto>> PostCategory(CategoryCreateOrUpdateDto newCategory)
        {
            var createdCategoryDto = await _categoryService.AddCategory(newCategory);
            return Created("", createdCategoryDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryPartialDto>> PutCategory(CategoryCreateOrUpdateDto genre, string id)
        {
            await _categoryService.UpdateCategory(genre, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryService.DeleteCategory(id);
            return NoContent();
        }
    }
}
