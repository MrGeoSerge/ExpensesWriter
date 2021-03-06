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
    public class FamilyCurrentMonthResultsViewModel : PersonalCurrentMonthResultsViewModel
    {
        public FamilyCurrentMonthResultsViewModel()
        {
            Title = "Family Current Month Results";
        }
        protected override async Task<ObservableCollection<CategoryExpense>> GetCategoryExpenses()
        {
            var result = await new MonthResultsService().GetFamilyCurrentMonthResults();

            return result;
        }

    }
}
