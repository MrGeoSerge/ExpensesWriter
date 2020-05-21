﻿using System;

namespace ExpensesWriter.Models
{
    public class Expense
    {
        public string Id { get; set; }
        public double Money { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ModificationDateTime { get; set; }

        public string UserId { get; set; }

        public int? BudgetItemId { get; set; }
        public BudgetItem BudgetItem { get; set; }


        public Expense()
        {
        }

        public Expense(Expense expense)
        {
            Id = expense.Id;
            Money = expense.Money;
            Name = expense.Name;
            Category = expense.Category;
            CreationDateTime = expense.CreationDateTime;
            ModificationDateTime = expense.ModificationDateTime;
            UserId = expense.UserId;
            BudgetItemId = expense.BudgetItemId;
            BudgetItem = expense.BudgetItem;
        }
    }
}