using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesWriter.Models
{
    public class EmailExpense
    {
        public double Money { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        public EmailExpense()
        {

        }


        public EmailExpense(Expense expense)
        {
            Money = expense.Money;
            Name = expense.Name;
            Category = expense.Category;
        }

        public override string ToString()
        {
            return Money.ToString() + " " + Name + " "  + Category;
        }

    }
}
