using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    public class ExpenseDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}
