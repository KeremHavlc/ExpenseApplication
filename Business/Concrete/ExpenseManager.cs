using Business.Abstract;
using Core.Dto;
using DataAccess.Abstract;
using Entity.Concrete;

namespace Business.Concrete
{
    public class ExpenseManager : IExpenseService
    {
        private readonly IExpenseDal _expenseDal;
        private readonly ICurrentUserService _currentUserService;
        public ExpenseManager(IExpenseDal expenseDal, ICurrentUserService currentUserService)
        {
            _expenseDal = expenseDal;
            _currentUserService = currentUserService;
        }

        public (bool success, string message) Add(ExpenseDto expenseDto)
        {
            var userId = _currentUserService.UserId;
            if (expenseDto.UserId != userId)
            {
                return (false, "Bu işlem için yetkiniz yok!");
            }
            var newExpense = new Expense
            {
                Title = expenseDto.Title,
                Description = expenseDto.Description,
                Amount = expenseDto.Amount,
                ExpenseDate = expenseDto.ExpenseDate,
                CategoryId = expenseDto.CategoryId,
                UserId = userId,
            };
            _expenseDal.Add(newExpense);
            return (true, "Ekleme işlemi başarılı!");
        }

        public (bool success, string message) Delete(Guid id)
        {
            var userId = _currentUserService.UserId;
            var expense = _expenseDal.Get(x => x.Id == id && x.UserId == userId);
            if(expense != null)
            {
                _expenseDal.Delete(expense);
                return (true, "Silme işlemi başarılı!");
            }
            return (false, "Silinecek harcama bulunamadı!");
        }

        public List<Expense> GetExpenseByCategoryId(Guid categoryId)
        {
            var userId = _currentUserService.UserId; // Kullanıcıyı alıyoruz
            var expenses = _expenseDal.GetAll(x => x.UserId == userId && x.CategoryId == categoryId); // Kullanıcının harcamaları alınacak

            // Eğer harcama yoksa, harcama bulunamadı mesajı döndürüyoruz
            if (expenses == null || !expenses.Any())
            {
                return null; // "Harcama bulunamadı" mesajı controller'da dönecek
            }

            // Eğer kullanıcıya ait harcama yoksa ve kullanıcı bilgisi uyuşmuyorsa, yetkisiz mesajı döndürüyoruz
            if (expenses.All(exp => exp.UserId != userId))
            {
                throw new UnauthorizedAccessException("Bu kategoriye ait harcamaları görüntüleme yetkiniz yok.");
            }

            return expenses;
        }

        public List<Expense> GetExpensesByUserId()
        {
            var userId = _currentUserService.UserId;
            var expenses = _expenseDal.GetAll(x => x.UserId == userId);
            if(expenses == null)
            {
                return null;
            }
            return expenses;
        }

        public (bool success, string message) Update(Guid id, ExpenseDto expenseDto)
        {
            var userId = _currentUserService.UserId;
            var expense = _expenseDal.Get(x => x.Id == id && x.UserId == userId);
            if (expense == null)
            {
                return (false, "Güncellenecek harcama bulunamadı!");
            }
            expense.Title = expenseDto.Title;
            expense.Description = expenseDto.Description;
            expense.Amount = expenseDto.Amount;
            expense.ExpenseDate = expenseDto.ExpenseDate;
            expense.CategoryId = expenseDto.CategoryId;
            expense.UserId = userId;
            _expenseDal.Update(expense);
            return (true, "Güncelleme işlemi başarılı!");
        }
    }
}
