using Business.Abstract;
using Core.Dto;
using DataAccess.Abstract;
using Entity.Concrete;

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

        public List<Expense> GetExpenseByCategoryId(Guid categoryId, Guid userId)
        {
            var expenses = _expenseDal.GetAll(x => x.UserId == userId && x.CategoryId == categoryId);

            if(expenses == null)
            {
                return null;
            }
            return expenses;
        }

        public List<Expense> GetExpensesByUserId(Guid userId)
        {
            var expenses = _expenseDal.GetAll(x => x.UserId == userId);
            if(expenses == null)
            {
                return null;
            }
            return expenses;
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
