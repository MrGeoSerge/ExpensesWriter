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

        public async Task<IEnumerable<Expense>> RenewLocalStorage()
        {
                var items = await externalStorage.GetItemsAsync(true);

                if (items.Count() > 0)
                {
                    await localStorage.AddItemsAsync(items);
                }
            return items;
        }

        public async Task CheckForExternalUpdates()
        {
            Expense lastModifiedLocalStorageExpense = await localStorage.GetLastModifiedItemAsync();
            DateTime lastModifiedLocalStorageDT = lastModifiedLocalStorageExpense.ModificationDateTime;


            Expense lastModifiedExternalStorageExpense = await externalStorage.GetLastModifiedItemAsync();
            DateTime lastModifiedExternalStorageDT = lastModifiedExternalStorageExpense.ModificationDateTime;

            if(lastModifiedExternalStorageDT > lastModifiedLocalStorageDT)
            {
                await UpdateLocalStorageWithExternallyChangedItems(lastModifiedLocalStorageDT);
            }
        }

        private async Task UpdateLocalStorageWithExternallyChangedItems(DateTime lastModifiedLocalStorageDT)
        {
            //find all expenses updated between last local update and now
            var expenses = await externalStorage.GetModifiedItemsAsync(lastModifiedLocalStorageDT);

            //var result = 
                await localStorage.AddItemsAsync(expenses);

            //if(result != expenses.Count())
            //{

            //    throw new Exception($"Items were not addedd. Expected to add {expenses.Count()}, but added {result}");
            //}

            //Notify view that items were changed!!!
            //Send message!!!
            MessagingCenter.Send<ExpenseService, ObservableCollection<Expense>>(this, "UpdateExpenses", null);
        }
    }
}