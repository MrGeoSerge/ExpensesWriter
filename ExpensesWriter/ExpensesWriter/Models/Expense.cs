using SQLite;
using SQLiteNetExtensions.Attributes;
using System;

namespace ExpensesWriter.Models
{
    [Table("Expenses")]
    public class Expense
    {
        [PrimaryKey]
        public string Id { get; set; }
        public double Money { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ModificationDateTime { get; set; }

        public string UserId { get; set; }

        [ForeignKey(typeof(BudgetItem))]
        public int? BudgetItemId { get; set; }

        [OneToOne]
        public BudgetItem BudgetItem { get; set; }

        public bool IsDeleted { get; set; }
        public bool SentUpdates { get; set; } = true;
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
            IsDeleted = expense.IsDeleted;
        }
    }
}