using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExpensesWriter.WebApi.Models
{
    public class ExpensesContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public ExpensesContext() : base("name=ExpensesWriterDB")
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<BudgetPlanningItem> BudgetPlanningItems { get; set; }

        //TODO: Create FamilyDB Set and family creation logic and UI
    }
}