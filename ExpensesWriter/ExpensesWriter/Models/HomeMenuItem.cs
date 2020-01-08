using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesWriter.Models
{
    public enum MenuItemType
    {
        ExpenseWriter,
        About,
        Logout
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
