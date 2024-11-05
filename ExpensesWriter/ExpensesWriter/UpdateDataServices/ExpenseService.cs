using ExpensesWriter.Enums;
using ExpensesWriter.Models;
using ExpensesWriter.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.UpdateServices
{
    public class ExpenseService
    {
        readonly Repositories.Local.ExpensesDataStore localStorage;
        readonly ExpensesDataService externalStorage;

        public UpdateStatusMessage StatusMessage { get; set; } = new UpdateStatusMessage();

        public ExpenseService()
        {
            localStorage = new Repositories.Local.ExpensesDataStore();
            externalStorage = new ExpensesDataService();
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            //get from local storage if there are some
            var items = await localStorage.GetItemsAsync();

            SetUpdateStatus("Got items from local storage!", Color.Green, UpdateStatus.InProgress);

            if (items.Count() == 0)
            {
                items = await RenewLocalStorage();
            }

            return items;
        }



        public async Task ProcessUpdates()
        {
            SetUpdateStatus("Update in progress", Color.Yellow, UpdateStatus.InProgress);

            await ResentNotSentExpenses();

            var expenses = await GetExternalUpdates();

            if(expenses != null)
            {
                await UpdateLocalStorageWithExternallyChangedItems(expenses);
            }
            SetUpdateStatus("Update completed", Color.Green, UpdateStatus.Completed);
        }

        private async Task ResentNotSentExpenses()
        {
            var expenses = await localStorage.GetPersonalUnsentCurrentMonthExpenses();

            foreach (var expense in expenses)
            {
                System.Diagnostics.Debug.WriteLine($"ResentNotSentExpenses expense {expense.Id} {expense.Name}");
                Expense addExpense = new Expense(expense);
                addExpense.BudgetItem = null;
                bool addResult = await externalStorage.AddItemAsync(addExpense);
                System.Diagnostics.Debug.WriteLine($"ResentNotSentExpenses expense {expense.Id} {expense.Name} addResult: {addResult}");

                if (addResult)
                {
                    expense.SentUpdates = true;
                    await localStorage.UpdateItemAsync(expense);
                }
            }

        }

        private async Task<IEnumerable<Expense>> RenewLocalStorage()
        {
            SetUpdateStatus("Renewing local storage", Color.Yellow, UpdateStatus.InProgress);

            var items = await externalStorage.GetItemsAsync(true);

            if (items.Count() > 0)
            {
                await localStorage.AddItemsAsync(items);
                items = await localStorage.GetItemsAsync();
            }
            return items;
        }

        private async Task<IEnumerable<Expense>> GetExternalUpdates()
        {
            DateTime lastModifiedLocalStorageDT;
            Expense lastModifiedLocalStorageExpense = await localStorage.GetLastModifiedItemAsync();
            if(lastModifiedLocalStorageExpense != null)
            {
                lastModifiedLocalStorageDT = lastModifiedLocalStorageExpense.ModificationDateTime;
            }
            else
            {
                lastModifiedLocalStorageDT = DateTime.MinValue;
            }

            Expense lastModifiedExternalStorageExpense = await externalStorage.GetLastModifiedItemAsync();
            DateTime lastModifiedExternalStorageDT = lastModifiedExternalStorageExpense.ModificationDateTime;

            SetUpdateStatus($"Last modified: {lastModifiedExternalStorageExpense.ModificationDateTime.ToShortTimeString()}", Color.YellowGreen, UpdateStatus.InProgress);

            if (lastModifiedExternalStorageDT > lastModifiedLocalStorageDT)
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

        public async Task AddExpenseAsync(Expense expense)
        {
            expense.SentUpdates = false;
            expense.ModificationDateTime = DateTime.Now;
            await localStorage.AddItemAsync(expense);

            Expense addExpense = new Expense(expense);
            addExpense.BudgetItem = null;
            bool addResult = await externalStorage.AddItemAsync(addExpense);

            if (addResult)
            {
                expense.SentUpdates = true;
                await localStorage.UpdateItemAsync(expense);
            }
        }

        public async Task UpdateExpense(Expense expense)
        {
            expense.SentUpdates = false;
            expense.ModificationDateTime = DateTime.Now;
            System.Diagnostics.Debug.WriteLine($"UpdateExpense {expense.Id} {expense.Name}: SentUpdates: {expense.SentUpdates}");

            await localStorage.UpdateItemAsync(expense);

            bool updateResult = await externalStorage.UpdateItemAsync(expense);
            if (updateResult)
            {
                expense.SentUpdates = true;
                await localStorage.UpdateItemAsync(expense);
                System.Diagnostics.Debug.WriteLine($"UpdateExpense externalStorage.UpdateItemAsync {expense.Id} {expense.Name}: SentUpdates: {expense.SentUpdates}");
            }
        }

        public async Task DeleteExpense(Expense expense)
        {
            expense.IsDeleted = true;
            await UpdateExpense(expense);
        }

        private void SetUpdateStatus(string message, Color color, UpdateStatus updateStatus)
        {
            StatusMessage.Message = message;
            StatusMessage.Color = color;
            StatusMessage.UpdateStatus = updateStatus;
            MessagingCenter.Send<ExpenseService, UpdateStatusMessage>(this, "UpdateStatusMessage", StatusMessage);

        }
    }
}