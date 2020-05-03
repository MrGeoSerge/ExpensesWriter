using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesWriter.Models
{
    public enum MenuItemType
    {
        ExpenseWriter,
        MonthResults,
        LastMonthResults,
        LastMonthExpenses,
        FamilyCurrentMonthExpenses,
        FamilyLastMonthExpenses,
        FamilyCurrentMonthResults,
        FamilyLastMonthResults,
        AllExpenses,
        About,
        Logout,
        Settings
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
