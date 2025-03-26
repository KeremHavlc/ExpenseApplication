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
            return Ok();
        }
    }
}
