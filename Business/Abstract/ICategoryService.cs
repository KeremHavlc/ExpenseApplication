using Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        void Add(CategoryDto categoryDto);
        void Delete(Guid categoryId);
        void Update(Guid categoryId , CategoryDto categoryDto);
        List<CategoryDto> GetAllCategoryByUserId(Guid userId);

    }
}
