using AutoMapper;
using StoreProject.BLL.Dtos.Category;
using StoreProject.BLL.Interfaces;
using StoreProject.DAL.Interfaces;
using StoreProject.DAL.Models;

namespace StoreProject.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryPartialDto>> GetCategories()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var categoriesDto = _mapper.Map<IEnumerable<CategoryPartialDto>>(categories);
            return categoriesDto;
        }
        public async Task<CategoryDto> GetCategory(string id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task<CategoryPartialDto> AddCategory(CategoryCreateDto newCategoryDto)
        {
            await CheckIfDuplicateNameExists(newCategoryDto.Name);
            //if the category doesn't exist, create new product in db
            var newCategory = _mapper.Map<Category>(newCategoryDto);
            await _unitOfWork.Categories.AddAsync(newCategory);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<CategoryPartialDto>(newCategory);
        }
        public async Task UpdateCategory(CategoryCreateDto categoryToUpdate, string id)
        {
            var existingCategory = await CheckIfCategoryExists(id);
            //check if the category with the same name already exists in db
            await CheckIfDuplicateNameExists(categoryToUpdate.Name, id);
            //if the category exists, update it in db
            _mapper.Map(categoryToUpdate, existingCategory);
            await _unitOfWork.Categories.UpdateAsync(existingCategory);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCategory(string id)
        {
            //check if the category exists in db
            var category = await CheckIfCategoryExists(id);
            //if the category exists, delete it from db
            await _unitOfWork.Categories.DeleteAsync(category);
            await _unitOfWork.SaveAsync();
        }

        private async Task<Category> CheckIfCategoryExists(string id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
            {
                // Category does not exist
                throw new ArgumentException($"Category with the ID ({id}) doesn't exist.");
            }
            return category;
        }
        private async Task CheckIfDuplicateNameExists(string name, string id = null)
        {
            var productsWithSameName = await _unitOfWork.Categories.FindAsync(p => p.Name == name && (id == null || p.Id != id));
            if (productsWithSameName.Any())
            {
                throw new ArgumentException($"Category with the same name ({name}) already exists.");
            }
        }
    }
}
