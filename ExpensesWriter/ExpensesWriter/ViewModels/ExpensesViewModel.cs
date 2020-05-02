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

namespace ExpensesWriter.ViewModels
{
    public class ExpensesViewModel : BaseExpenseViewModel
    {

        public ObservableCollection<Expense> expenses1;
        public ObservableCollection<Expense> Expenses
        {
            get
            {
                return expenses1;
            }
            set
            {
                expenses1 = value;
                OnPropertyChanged();
            }
        }


        public Command LoadExpensesCommand { get; set; }
        public Command EmailExpensesCommand { get; set; }


        public ExpensesViewModel()
        {
            Title = "Browse";
            Expenses = new ObservableCollection<Expense>();

            LoadExpensesCommand = new Command(async () => await ExecuteLoadExpensesCommand());

            EmailExpensesCommand = new Command(async () => await ExecuteEmailExpensesCommand());

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

            MessagingCenter.Subscribe<CurrentMonthExpensesPage, Expense>(this, "AddExpense3", async (obj, expense) =>
            {
                var newExpense = expense as Expense;
                Expenses.Insert(0, newExpense);
                await DataStore.AddItemAsync(newExpense);
            });

            LoadExpensesCommand.Execute(null);
        }

        protected async Task ExecuteLoadExpensesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Expenses.Clear();
                var expenses = await GetExpenses();
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
                IsBusy = false;
            }
        }

        protected virtual async Task<IEnumerable<Expense>> GetExpenses()
        {
            return await DataStore.GetItemsAsync(true);
        }

        private async Task ExecuteEmailExpensesCommand()
        {
            var emailService = new EmailExpensesService();
            await emailService.SendExpenses(Expenses);
        }



    }
}