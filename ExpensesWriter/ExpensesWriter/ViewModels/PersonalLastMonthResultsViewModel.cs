﻿using ExpensesWriter.Models;
using ExpensesWriter.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesWriter.ViewModels
{
    public class PersonalLastMonthResultsViewModel : PersonalCurrentMonthResultsViewModel
    {
        public PersonalLastMonthResultsViewModel()
        {
            Title = "Last Month Results";
        }
        protected override async Task<ObservableCollection<CategoryExpense>> GetCategoryExpenses()
        {
            return await new MonthResultsService().GetLastMonthResults();
        }
    }
}
