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
    public class ExpensesController : ControllerBase       
    {
        private readonly IExpenseService _expenseService;
        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;   
        }


        [HttpPost("AddExpense")]
        public IActionResult AddExpense(ExpenseDto expenseDto)
        {
            _expenseService.Add(expenseDto);
            return Ok("Başarıyla Eklendi!");
        }

        [HttpPost("DeleteExpense")]
        public IActionResult DeleteExpense(Guid id)
        {
            _expenseService.Delete(id);
            return Ok("Başarıyla Silindi!");
        }
        [HttpPost("UpdateExpense/{id}")]
        public IActionResult UpdateExpense([FromRoute(Name = "id")] Guid id, [FromBody] ExpenseDto expenseDto)
        {
            _expenseService.Update(id, expenseDto);
            return Ok("Başarıyla Güncellendi!");
        }

        [HttpGet("GetExpensesAllByUserId/{id}")]
        public IActionResult GetAllExpensesByUserId([FromRoute] Guid id)
        {
            var expenses = _expenseService.GetExpensesByUserId(id);

            if (expenses == null || !expenses.Any())
            {
                return NotFound("Bu kullanıcıya ait harcama bulunamadı.");
            }

            return Ok(expenses);
        }
        [HttpGet("GetExpensesByCategoryId/{categoryId}/{userId}")]
        public IActionResult GetExpensesByCategoryId([FromRoute] Guid categoryId, [FromRoute] Guid userId)
        {
            var expenses = _expenseService.GetExpenseByCategoryId(categoryId, userId);

            if (expenses == null || !expenses.Any()) // Liste boşsa NotFound dön
            {
                return NotFound("Bu kategoriye ait harcama bulunamadı!");
            }

            return Ok(expenses);
        }
    }
}
