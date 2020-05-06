using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpensesWriter.WebApi.Models
{
    public class BudgetItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }

        public string UserId { get; set; }

        //public ICollection<Expense> Expenses { get; set; }
    }
}