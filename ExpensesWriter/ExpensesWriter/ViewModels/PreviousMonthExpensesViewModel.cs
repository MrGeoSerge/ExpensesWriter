using ExpensesWriter.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class PreviousMonthExpensesViewModel : ExpensesViewModel
    {
        public Command LoadPreviousMonthExpensesCommand { get; set; }

        public PreviousMonthExpensesViewModel()
        {
            LoadPreviousMonthExpensesCommand = new Command(async () => await ExecuteLoadPreviousMonthExpensesCommand());
            LoadPreviousMonthExpensesCommand.Execute(null);
        }

        private async Task ExecuteLoadPreviousMonthExpensesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Expenses.Clear();
                var expenses = await DataStore.GetPreviousMonthItemsAsync(true);
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
