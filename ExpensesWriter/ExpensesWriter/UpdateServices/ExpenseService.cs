using ExpensesWriter.Models;
using ExpensesWriter.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.UpdateServices
{
    public class ExpenseService
    {
        readonly Repositories.Local.ExpensesDataStore localStorage;
        readonly AzureDataStore externalStorage;

        public ExpenseService()
        {
            localStorage = new Repositories.Local.ExpensesDataStore();
            externalStorage = new AzureDataStore();
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            //get from local storage if there are some
            var items = await localStorage.GetItemsAsync();

            if (items.Count() == 0)
            {
                items = await RenewLocalStorage();
            }

            return items;
        }

        public async Task ProcessUpdates()
        {
            var expenses = await GetExternalUpdates();

            if(expenses != null)
            {
                await UpdateLocalStorageWithExternallyChangedItems(expenses);
                await UpdateExpensesInView();
            }
        }

        private async Task<IEnumerable<Expense>> RenewLocalStorage()
        {
            var items = await externalStorage.GetItemsAsync(true);

            if (items.Count() > 0)
            {
                await localStorage.AddItemsAsync(items);
            }
            return items;
        }

        private async Task<IEnumerable<Expense>> GetExternalUpdates()
        {
            Expense lastModifiedLocalStorageExpense = await localStorage.GetLastModifiedItemAsync();
            DateTime lastModifiedLocalStorageDT = lastModifiedLocalStorageExpense.ModificationDateTime;

            Expense lastModifiedExternalStorageExpense = await externalStorage.GetLastModifiedItemAsync();
            DateTime lastModifiedExternalStorageDT = lastModifiedExternalStorageExpense.ModificationDateTime;

            if(lastModifiedExternalStorageDT > lastModifiedLocalStorageDT)
            {
                var expenses = await externalStorage.GetModifiedItemsAsync(lastModifiedLocalStorageDT);
                return expenses;
            }
            else
            {
                return null;
            }
        }

        private async Task UpdateLocalStorageWithExternallyChangedItems(IEnumerable<Expense> expenses)
        {
                await localStorage.AddItemsAsync(expenses);
        }

        private async Task UpdateExpensesInView()
        {
            var expenses = await localStorage.GetItemsAsync();
            var sortedExpenses = expenses.Cast<Expense>().OrderByDescending((x) => x.CreationDateTime).Select(x => x);
            var expensesCollection = new ObservableCollection<Expense>(sortedExpenses);

            MessagingCenter.Send<ExpenseService, ObservableCollection<Expense>>(this, "UpdateExpenses", expensesCollection);
        }
    }
}