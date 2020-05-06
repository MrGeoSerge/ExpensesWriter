using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpensesWriter.WebApi.Models
{
    public class Expense
    {
        public string Id { get; set; }

        [Required]
        public double Money { get; set; }

        [Required]
        public string Name { get; set; }

        public string CategoryName { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ModificationDateTime { get; set; }

        public string UserId { get; set; }

        public int? BudgetItemId { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
    }
}