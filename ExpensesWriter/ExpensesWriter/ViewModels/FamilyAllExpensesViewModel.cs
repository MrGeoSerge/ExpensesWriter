using ExpensesWriter.Models;
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
            Title = "All Expenses";
            SubscribeForUpdates();
        }

        protected override async Task<IEnumerable<Expense>> GetExpenses()
        {
            var expensesService = new ExpenseService();
            var expenses = await expensesService.GetExpensesAsync();

            Task.Run(() => expensesService.ProcessUpdates());

            return expenses;
        }

        private void SubscribeForUpdates()
        {
            MessagingCenter.Subscribe<ExpenseService, ObservableCollection<Expense>>(this, "UpdateExpenses", (obj, args) =>
            {
                var expenses = args as IEnumerable<Expense>;
                Expenses = new ObservableCollection<Expense>(expenses);
            });
        }
    }
}
