using Business.Abstract;
using Core.Dto;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        private readonly ICurrentUserService _currentUserService;
        private readonly IExpenseDal _expenseDal;
        public CategoryManager(ICategoryDal categoryDal , ICurrentUserService currentUserService, IExpenseDal expenseDal)
        {
            _categoryDal = categoryDal;
            _currentUserService = currentUserService;
            _expenseDal = expenseDal;
        }

        public (bool success , string message) Add(CategoryDto categoryDto)
        {
            var userId = _currentUserService.UserId;
            if(categoryDto.UserId != userId)
            {
                return (false, "Bu işlem için yetkiniz yok!");  
            }
            var newCategory = new Category
            {
                Id = categoryDto.CategoryId,
                CategoryName = categoryDto.CategoryName,
                UserId = userId
            };
            _categoryDal.Add(newCategory);
            return (true, "Kategori Ekleme işlemi başarılı!");
        }

        public (bool success, string message) Delete (Guid categoryId)
        {
           var userId = _currentUserService.UserId;
            var category = _categoryDal.Get(x => x.Id == categoryId && x.UserId == userId);
            if (category == null)
            {
                return (false, "silinecek kategori bulunamadı!");
            }

            bool expenses = _expenseDal.GetAll(x => x.CategoryId == categoryId).Any();
            if (expenses)
            {
                return (false, "Bu kategoride harcama bulunduğu için silemezsiniz!");
            }
            _categoryDal.Delete(category);
            return (true, "Silme işlemi başarılı!");
        }

        public List<Category> GetAllCategoryByUserId()
        {
            var userId = _currentUserService.UserId;
            var categories = _categoryDal.GetAll(x => x.UserId == userId);
            if (categories == null)
            {
                return null;
            }
            return categories;
        }

        public (bool success, string message) Update(Guid categoryId, CategoryDto categoryDto)
        {
            var userId = _currentUserService.UserId;
            var category = _categoryDal.Get(x => x.Id == categoryId && x.UserId == userId);
            if(category == null)
            {
                return (false, "Güncellenecek kategori bulunamadı!");
            }
            category.CategoryName = categoryDto.CategoryName;
            _categoryDal.Update(category);
            return (true, "Güncelleme işlemi başarılı!");

        }
    }
}
