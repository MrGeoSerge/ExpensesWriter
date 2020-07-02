using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesWriter.Models
{

    [Table("BudgetPlanningItem")]
    public class BudgetPlanningItem
    {
        [PrimaryKey]
        public int Id { get; set; }
        public int BudgetItemId { get; set; }
        public virtual BudgetItem BudgetItem { get; set; }
        public double Money { get; set; }
        public DateTime PlanningMonth { get; set; }
        public string UserId { get; set; }

    }
}
