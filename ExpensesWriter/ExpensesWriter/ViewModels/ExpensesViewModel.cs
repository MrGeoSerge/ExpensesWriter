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

namespace ExpensesWriter.ViewModels
{
    public class ExpensesViewModel : BaseExpenseViewModel
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

        public Command LoadExpensesCommand { get; set; }
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

            MessagingCenter.Subscribe<NewExpensePage, Expense>(this, "AddExpense", async (obj, expense) =>
            {
                var newExpense = expense as Expense;
                Expenses.Add(newExpense);
                await DataStore.AddItemAsync(newExpense);
            });

            MessagingCenter.Subscribe<AllExpensesPage, Expense>(this, "AddExpense2", async (obj, expense) =>
            {
                var newExpense = expense as Expense;
                Expenses.Insert(0, newExpense);
                await DataStore.AddItemAsync(newExpense);
            });

            LoadExpensesCommand.Execute(null);
        }

        private async Task ExecuteAddExpenseCommand(object expense)
        {
            var newExpense = expense as Expense;
            if(newExpense != null)
            {
                Expenses.Insert(0, newExpense);

                Expense addExpense = new Expense(newExpense);
                addExpense.BudgetItem = null;
                await DataStore.AddItemAsync(addExpense);
            }
        }

        protected virtual async Task ExecuteLoadExpensesCommand()
        {
            if (IsBusy)
                return;

            Device.BeginInvokeOnMainThread(() =>
            IsBusy = true);

            try
            {
                Expenses.Clear();
                var expenses = await GetExpenses().ConfigureAwait(true);
                var sortedExpenses = expenses.Cast<Expense>().OrderByDescending((x) => x.CreationDateTime).Select(x => x);
                foreach(var expense in sortedExpenses)
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
                Device.BeginInvokeOnMainThread(() =>
                    IsBusy = false);
            }
        }

        protected virtual async Task<IEnumerable<Expense>> GetExpenses()
        {
            //var expensesService = new ExpenseService();
            //var expenses = await expensesService.GetExpensesAsync();


            return await DataStore.GetItemsAsync(true);
        }

        private async Task ExecuteEmailExpensesCommand()
        {
            var emailService = new EmailExpensesService();
            await emailService.SendExpenses(Expenses);
        }



    }
}