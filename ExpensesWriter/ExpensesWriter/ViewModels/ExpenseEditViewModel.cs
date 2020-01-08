using System;
using System.Windows.Input;
using ExpensesWriter.Models;
using ExpensesWriter.Services;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class ExpenseEditViewModel : BaseExpenseViewModel
    {
        public Expense Expense { get; set; }



        public ICommand SaveCommand
        {
            get
            {
                return new Command( () =>
                {
                    //IDataStore<Expense> DataStore = DependencyService.Get<IDataStore<Expense>>();
                    //await DataStore.UpdateItemAsync(Expense);
                    //await App.Current.MainPage.Navigation.PopAsync();
                    //App.Current.
                });
            }
        }
        public ExpenseEditViewModel(Expense expense = null)
        {
            Title = expense?.Name;
            Expense = expense;
        }
    }
}
