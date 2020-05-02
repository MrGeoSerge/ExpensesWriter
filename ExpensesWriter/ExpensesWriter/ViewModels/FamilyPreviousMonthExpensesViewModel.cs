using ExpensesWriter.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class FamilyPreviousMonthExpensesViewModel : ExpensesViewModel
    {
        public Command LoadFamilyPreviousMonthExpensesCommand { get; set; }

        public FamilyPreviousMonthExpensesViewModel()
        {
            LoadFamilyPreviousMonthExpensesCommand = new Command(async () => await ExecuteLoadFamilyPreviousMonthExpensesCommand());
            LoadFamilyPreviousMonthExpensesCommand.Execute(null);
        }

        private async Task ExecuteLoadFamilyPreviousMonthExpensesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Expenses.Clear();
                var expenses = await DataStore.GetFamilyPreviousMonthItemsAsync(true);
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
