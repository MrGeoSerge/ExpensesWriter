using System;

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
    }
}