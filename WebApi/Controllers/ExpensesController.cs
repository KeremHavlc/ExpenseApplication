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
            var res = _expenseService.Add(expenseDto);
            if(res.success == false)
            {
                return BadRequest(res.message);
            }
            return Ok(res.message);
        }

        [HttpPost("DeleteExpense")]
        public IActionResult DeleteExpense(Guid id)
        {
            var res = _expenseService.Delete(id);
            if (res.success == false)
            {
                return BadRequest(res.message);
            }
            return Ok(res.message);
        }
        [HttpPost("UpdateExpense/{id}")]
        public IActionResult UpdateExpense([FromRoute(Name = "id")] Guid id, [FromBody] ExpenseDto expenseDto)
        {
            var res = _expenseService.Update(id, expenseDto);
            if(res.success == false)
            {
                return BadRequest(res.message);
            }
            return Ok(res.message);
        }

        [HttpGet("GetExpensesAllByUserId")]
        public IActionResult GetAllExpensesByUserId()
        {
            var expenses = _expenseService.GetExpensesByUserId();

            if (expenses == null || !expenses.Any())
            {
                return NotFound("Bu kullanıcıya ait harcama bulunamadı.");
            }

            return Ok(expenses);
        }
        [HttpGet("GetExpensesByCategoryId/{categoryId}")]
        public IActionResult GetExpensesByCategoryId([FromRoute] Guid categoryId)
        {
            try
            {
                var expenses = _expenseService.GetExpenseByCategoryId(categoryId); 

                if (expenses == null || !expenses.Any()) // Liste boşsa NotFound dön
                {
                    return NotFound("Bu kategoriye ait harcama bulunamadı!"); 
                }

                return Ok(expenses); // Harcama varsa geri döndür
            }
            catch (UnauthorizedAccessException ex) // Yetkisiz erişim durumunda hata mesajı
            {
                return Unauthorized(ex.Message); // "Yetkiniz yok" mesajını döndürüyoruz Managerdan
            }
        }
    }
}
