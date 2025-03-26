using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public Guid UserId { get; set; }
        public string CategoryName { get; set; }
    }
}
