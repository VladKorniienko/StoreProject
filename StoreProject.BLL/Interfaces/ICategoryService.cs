using StoreProject.BLL.Dtos.Category;
using StoreProject.BLL.Dtos.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.BLL.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryPartialDto>> GetCategories();
        public Task<CategoryDto> GetCategory(string id);
        public Task<CategoryPartialDto> AddCategory(CategoryCreateOrUpdateDto newCategoryDto);
        public Task UpdateCategory(CategoryCreateOrUpdateDto categoryToUpdate, string id);
        public Task DeleteCategory(string id);
    }
}
