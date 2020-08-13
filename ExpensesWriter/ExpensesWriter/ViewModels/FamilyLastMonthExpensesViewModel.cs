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
    public class FamilyLastMonthExpensesViewModel : ExpensesViewModel
    {
        public FamilyLastMonthExpensesViewModel()
        {
            Title = "Family Last Month Expenses";
        }

        protected override async Task<IEnumerable<Expense>> GetExpenses()
        {
            try
            {
                var expenses = await new ExpensesDataStore().GetPersonalCurrentMonthExpenses();
                return expenses;
            }
            catch (Exception ex)
            {
                ShowMessage("Family Last Month Expenses Error", ex.Message);
                return null;
            }
        }


    }
}
