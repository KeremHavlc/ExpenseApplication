using Business.Abstract;
using Core.Dto;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal; 
        }

        public void Add(CategoryDto categoryDto)
        {
            var newCategory = new Category
            {
                Id = categoryDto.CategoryId,
                CategoryName = categoryDto.CategoryName,
                UserId = categoryDto.UserId
            };
            _categoryDal.Add(newCategory);
        }

        public void Delete(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDto> GetAllCategoryByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid categoryId, CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
