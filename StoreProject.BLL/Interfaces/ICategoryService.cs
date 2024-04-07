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
        public Task<CategoryPartialDto> AddCategory(CategoryCreateDto newCategoryDto);
        public Task<bool> UpdateCategory(CategoryCreateDto categoryToUpdate, string id);
        public Task<bool> DeleteCategory(string id);
    }
}
