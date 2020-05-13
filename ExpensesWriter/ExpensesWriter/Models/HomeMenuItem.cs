using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesWriter.Models
{
    public enum MenuItemType
    {
        ExpenseWriter,
        MonthResults,
        MonthPlanning,
        LastMonthResults,
        LastMonthExpenses,
        FamilyCurrentMonthExpenses,
        FamilyCurrentMonthResults,
        FamilyLastMonthExpenses,
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
