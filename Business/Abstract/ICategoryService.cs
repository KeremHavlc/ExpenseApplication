using Core.Dto;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        (bool success , string message) Add(CategoryDto categoryDto);
        (bool success, string message) Delete(Guid categoryId);
        (bool success, string message) Update(Guid categoryId , CategoryDto categoryDto);
        List<Category> GetAllCategoryByUserId();

    }
}
