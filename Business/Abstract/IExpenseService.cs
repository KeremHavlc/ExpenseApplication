using Core.Dto;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IExpenseService
    {
        (bool success, string message) Add(ExpenseDto expenseDto);
        (bool success, string message) Update(Guid id,ExpenseDto expenseDto);
        (bool success, string message) Delete(Guid id);
        List<Expense> GetExpensesByUserId();
        List<Expense> GetExpenseByCategoryId(Guid categoryId);

    }
}
