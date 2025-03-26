using Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IExpenseService
    {
        void Add(ExpenseDto expenseDto);
        void Update(Guid id,ExpenseDto expenseDto);
        void Delete(Guid id);
        List<ExpenseDto> GetExpensesByUserId(Guid userId);
        List<ExpenseDto> GetExpenseByCategoryId(Guid categoryId , Guid userId);

    }
}
