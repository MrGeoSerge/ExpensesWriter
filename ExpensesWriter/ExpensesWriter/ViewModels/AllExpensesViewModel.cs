﻿using ExpensesWriter.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class AllExpensesViewModel : ExpensesViewModel
    {
        public AllExpensesViewModel()
        {
            Title = "All Expenses";
        }

        protected override async Task<IEnumerable<Expense>> GetExpenses()
        {
            return await DataStore.GetItemsAsync(true);
        }

    }
}