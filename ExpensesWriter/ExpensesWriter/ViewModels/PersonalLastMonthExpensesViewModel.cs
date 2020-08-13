using ExpensesWriter.Models;
using ExpensesWriter.Repositories.Local;
using ExpensesWriter.UpdateServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class PersonalLastMonthExpensesViewModel : ExpensesViewModel
    {

        public PersonalLastMonthExpensesViewModel()
        {
            Title = "Personal Last Month Expenses";
        }

        protected override async Task<IEnumerable<Expense>> GetExpenses()
        {
            try
            {
                var expenses = await new ExpensesDataStore().GetPersonalLastMonthExpenses();
                return expenses;
            }
            catch (Exception ex)
            {
                ShowMessage("Personal Last Month Expenses Error", ex.Message);
                return null;
            }
        }


    }
}
