using ExpensesWriter.Models;
using ExpensesWriter.UpdateServices;
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
            var expensesService = new ExpenseService();
            var expenses = await expensesService.GetExpensesAsync();

            //Task.Run(() => expensesService.CheckForExternalUpdates());
            //return await DataStore.GetItemsAsync(true);
            //IsBusy = false;
            return expenses;
        }

    }
}
