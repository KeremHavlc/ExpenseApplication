using Business.Abstract;
using Core.Dto;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ExpenseManager : IExpenseService
    {
        private readonly IExpenseDal _expenseDal;
        public ExpenseManager(IExpenseDal expenseDal)
        {
            _expenseDal = expenseDal;
        }

        public void Add(ExpenseDto expenseDto)
        {
            var newExpense = new Expense
            {
                Id = Guid.NewGuid(),
                Title = expenseDto.Title,
                Description = expenseDto.Description,
                Amount = expenseDto.Amount,
                ExpenseDate = expenseDto.ExpenseDate,
                CategoryId = expenseDto.CategoryId,
                UserId = expenseDto.UserId,
            };
            _expenseDal.Add(newExpense);
        }

        public void Delete(Guid id)
        {
            var expense = _expenseDal.Get(x => x.Id == id);
            if(expense != null)
            {
                _expenseDal.Delete(expense);
            }
        }

        public ExpenseDto GetExpenseById(Guid id)
        {
            var expense = _expenseDal.Get(e => e.Id == id);
            if (expense == null) return null;
            return new ExpenseDto
            {                
                Title = expense.Title,
                Description = expense.Description,
                Amount = expense.Amount,
                ExpenseDate = expense.ExpenseDate,
                CategoryId = expense.CategoryId,
                UserId = expense.UserId
            };
        }

        public List<ExpenseDto> GetExpenses()
        {
           var expense = _expenseDal.GetAll();
            return expense.Select(expense => new ExpenseDto
            {
                Title = expense.Title,
                Description = expense.Description,
                Amount = expense.Amount,
                ExpenseDate = expense.ExpenseDate,
                CategoryId = expense.CategoryId,
                UserId = expense.UserId
            }).ToList();
        }

        public void Update(Guid id, ExpenseDto expenseDto)
        {
            var expense = _expenseDal.Get(x => x.Id == id);
            if (expense == null) return;
            expense.Title = expenseDto.Title;
            expense.Description = expenseDto.Description;
            expense.Amount = expenseDto.Amount;
            expense.ExpenseDate = expenseDto.ExpenseDate;
            expense.CategoryId = expenseDto.CategoryId;
            expense.UserId = expenseDto.UserId;
            _expenseDal.Update(expense);
        }
    }
}
