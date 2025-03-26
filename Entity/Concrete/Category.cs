namespace Entity.Concrete
{
    public class Category :BaseEntity
    {
        public string CategoryName { get; set; }
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public Guid UserId { get; set; }
    }
}
