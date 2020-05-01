using ExpensesWriter.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class CurrentMonthExpensesViewModel : ExpensesViewModel
    {
        public Command LoadCurrentMonthExpensesCommand { get; set; }

        public CurrentMonthExpensesViewModel()
        {
            LoadCurrentMonthExpensesCommand = new Command(async () => await ExecuteLoadCurrentMonthExpensesCommand());
        }

        private async Task ExecuteLoadCurrentMonthExpensesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Expenses.Clear();
                var expenses = await DataStore.GetCurrentMonthItemsAsync(true);
                var sortedExpenses = expenses.Cast<Expense>().OrderByDescending((x) => x.CreationDateTime).Select(x => x);
                foreach (var expense in sortedExpenses)
                {
                    Expenses.Add(expense);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
