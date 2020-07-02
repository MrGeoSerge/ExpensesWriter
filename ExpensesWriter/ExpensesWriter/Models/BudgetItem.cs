using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesWriter.Models
{
    [Table("BudgetItem")]
    public class BudgetItem
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public string UserId { get; set; }

    }
}
