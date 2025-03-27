using Business.Abstract;
using Core.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(CategoryDto categoryDto)
        {
            var res = _categoryService.Add(categoryDto);
            if(res.success == false)
            {
                return BadRequest(res.message);
            }
            return Ok(res.message);
        }

        [HttpPost("DeleteCategory")]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            var res = _categoryService.Delete(categoryId);
            if (res.success == false)
            {
                return BadRequest(res.message);
            }
            return Ok(res.message);
        }

        [HttpPost("UpdateCategory/{id}")]
        public IActionResult UpdateCategory([FromRoute(Name = "id")] Guid categoryId, [FromBody] CategoryDto categoryDto)
        {
            var res = _categoryService.Update(categoryId, categoryDto);
            if (res.success == false)
            {
                return BadRequest(res.message);
            }
            return Ok(res.message);
        }

        [HttpGet("GetAllCategories")]
        public IActionResult GetAllCategories()
        {
            var categories = _categoryService.GetAllCategoryByUserId();
            if (categories == null || !categories.Any())
            {
                return NotFound("Bu kullanıcıya ait kategori bulunamadı.");
            }
            return Ok(categories);
        }

    }
}
