﻿using ExpensesWriter.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesWriter.ViewModels
{
    public class PreviousMonthResultsViewModel : MonthResultsViewModel
    {

        protected override async Task ExecuteLoadMonthResultsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                CategoryExpenses = await (new MonthResultsService().GetPreviousMonthResults());
                TotalMoney = CategoryExpenses.Sum(expense => expense.Money);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }


        }

    }
}
