using ExpensesWriter.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class FamilyCurrentMonthExpensesViewModel : ExpensesViewModel
    {
        public Command LoadFamilyCurrentMonthExpensesCommand { get; set; }

        public FamilyCurrentMonthExpensesViewModel()
        {
            LoadFamilyCurrentMonthExpensesCommand = new Command(async () => await ExecuteLoadFamilyCurrentMonthExpensesCommand());
            LoadFamilyCurrentMonthExpensesCommand.Execute(null);
        }

        private async Task ExecuteLoadFamilyCurrentMonthExpensesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Expenses.Clear();
                var expenses = await DataStore.GetFamilyCurrentMonthItemsAsync(true);
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
