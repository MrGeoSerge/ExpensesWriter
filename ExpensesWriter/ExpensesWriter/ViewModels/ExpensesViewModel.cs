using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using Xamarin.Forms;

using ExpensesWriter.Models;
using ExpensesWriter.Views;
using Xamarin.Essentials;
using ExpensesWriter.Services;
using System.Collections;
using System.Collections.Generic;
using ExpensesWriter.UpdateServices;
using System.Threading;

namespace ExpensesWriter.ViewModels
{
    public abstract class ExpensesViewModel : BaseViewModel
    {
        private ObservableCollection<Expense> expenses;
        public ObservableCollection<Expense> Expenses
        {
            get
            {
                return expenses;
            }
            set
            {
                expenses = value;
                OnPropertyChanged();
            }
        }

        private UpdateStatusMessage statusMessage;
        public UpdateStatusMessage StatusMessage
        {
            get
            {
                return statusMessage;
            }
            set
            {
                statusMessage = value;
                OnPropertyChanged();
            }
        }




        public Command LoadExpensesCommand { get; set; }
        public Command LoadUpdatesCommand { get; set; }
        public Command EmailExpensesCommand { get; set; }
        public Command AddExpenseCommand { get; set; }
        public Command DeleteExpenseCommand { get; set; }

        public ExpensesViewModel()
        {
            Title = "Browse";
            Expenses = new ObservableCollection<Expense>();

            LoadExpensesCommand = new Command(async () => await ExecuteLoadExpensesCommand());
            EmailExpensesCommand = new Command(async () => await ExecuteEmailExpensesCommand());
            AddExpenseCommand = new Command(async (expense) => await ExecuteAddExpenseCommand(expense));
            LoadUpdatesCommand = new Command(async () => await ExecuteLoadUpdatesCommand());

            SubscribeForEvents();

            LoadExpensesCommand.Execute(null);
            //LoadUpdatesCommand.Execute(null);
        }

        private async Task ExecuteAddExpenseCommand(object expense)
        {
            var newExpense = expense as Expense;
            if(newExpense != null)
            {
                Expenses.Insert(0, newExpense);

                await new ExpenseService().AddExpenseAsync(newExpense);
                //Expense addExpense = new Expense(newExpense);
                //addExpense.BudgetItem = null;
                //await AzureDataStore.AddItemAsync(addExpense);
            }
        }

        protected virtual async Task ExecuteLoadExpensesCommand()
        {
            if (IsBusy)
                return;

            Device.BeginInvokeOnMainThread(() => IsBusy = true);

            try
            {
                await UpdateExpensesInView();
            }
            catch (Exception ex)
            {
                ShowMessage("Expenses View Model Error", ex.Message);
            }
            finally
            {
                Device.BeginInvokeOnMainThread(() => IsBusy = false);
            }
        }

        protected abstract Task<IEnumerable<Expense>> GetExpenses();

        protected void CheckIsBusy()
        {
            Thread.Sleep(2000);
            ShowMessage("Is Busy", $"IsBusy = {IsBusy}");
        }

        private async Task ExecuteEmailExpensesCommand()
        {
            var emailService = new EmailExpensesService();
            await emailService.SendExpenses(Expenses);
        }

        private async Task UpdateExpensesInView()
        {
            Expenses.Clear();
            var expenses = await GetExpenses();
            var sortedExpenses = expenses.Cast<Expense>().OrderByDescending((x) => x.CreationDateTime).Select(x => x);
            foreach (var expense in sortedExpenses)
            {
                Expenses.Add(expense);
            }
            //Expenses = new ObservableCollection<Expense>(sortedExpenses);

        }

        private async Task ExecuteLoadUpdatesCommand()
        {
            await new ExpenseService().ProcessUpdates();
            await UpdateExpensesInView();
            IsBusy = false;
        }

        private void SubscribeForEvents()
        {
            MessagingCenter.Subscribe<NewExpensePage, Expense>(this, "AddExpense", async (obj, expense) =>
            {
                var newExpense = expense as Expense;
                Expenses.Add(newExpense);
                await AzureDataStore.AddItemAsync(newExpense);
            });

            MessagingCenter.Subscribe<FamilyAllExpensesPage, Expense>(this, "AddExpense2", async (obj, expense) =>
            {
                var newExpense = expense as Expense;
                Expenses.Insert(0, newExpense);
                await AzureDataStore.AddItemAsync(newExpense);
            });


            MessagingCenter.Subscribe<ExpenseService, UpdateStatusMessage>(this, "UpdateStatusMessage", (obj, args) =>
            {
                UpdateStatusMessage updateStatusMessage = args as UpdateStatusMessage;
                if (updateStatusMessage != null)
                {
                    if(updateStatusMessage.UpdateStatus == Enums.UpdateStatus.Completed)
                    {
                        
                    }
                    StatusMessage = updateStatusMessage;
                }
            });


        }


    }
}