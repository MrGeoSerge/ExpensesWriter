using ExpensesWriter.Models;
using ExpensesWriter.Repositories.Local;
using ExpensesWriter.UpdateServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class FamilyAllExpensesViewModel : ExpensesViewModel
    {

        public FamilyAllExpensesViewModel()
        {
            Title = "Family All Expenses";
        }

        protected override async Task<IEnumerable<Expense>> GetExpenses()
        {
            try
            {
                var expenses = await new ExpensesDataStore().GetFamilyAllExpenses();
                return expenses;
            }
            catch (Exception ex)
            {
                ShowMessage("Family All Expenses Error", ex.Message);
                return null;
            }
        }

    }
}
