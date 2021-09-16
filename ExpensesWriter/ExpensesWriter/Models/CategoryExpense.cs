using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesWriter.Models
{
    public class CategoryExpense
    {
        public string Category { get; set; }
        public double Money { get; set; }
        public double PlannedMoney { get; set; }
        public double LeftMoney => PlannedMoney - Money;
        public int PercentOfExecution { get; set; }
    }
}
