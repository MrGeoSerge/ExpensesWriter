using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ExpensesWriter.Models;
using ExpensesWriter.Views;
using ExpensesWriter.ViewModels;
using ExpensesWriter.Helpers;
using System.Text.RegularExpressions;

namespace ExpensesWriter.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ExpensesPage : ContentPage
    {
        ExpensesViewModel viewModel;

        public ExpensesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ExpensesViewModel();
        }

        async void OnExpenseSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var expense = args.SelectedItem as Expense;
            if (expense == null)
                return;

            await Navigation.PushAsync(new ExpenseEditPage(new ExpenseEditViewModel(expense)));

            // Manually deselect expense.
            ExpensesListView.SelectedItem = null;
        }

        async void AddExpense_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewExpensePage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.Expenses.Count == 0)
                viewModel.LoadExpensesCommand.Execute(null);

            expenseQntEntry.Focus();

        }

        private void ExpenseQntEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if(e.NewTextValue != string.Empty)
            {
                //if user types Space cursor moves to expenseNameEntry
                char lastChar = e.NewTextValue.Last();
                if (lastChar == ' ')
                {
                    expenseNameEntry.Focus();
                    return;
                }

                if (!lastChar.IsNumeric())
                {
                    DisplayAlert("Alert", "Only digits and . or , allowed", "Got it!");
                    expenseQntEntry.Text = e.OldTextValue;
                }

            }
        }

        private void ExpenseNameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            if (string.IsNullOrEmpty(expenseQntEntry.Text) || string.IsNullOrEmpty(expenseNameEntry.Text))
                return;

            Expense expense = new Expense();
            double.TryParse(expenseQntEntry.Text, out double expenseMoney);
            expense.Money = expenseMoney;
            expense.Name = expenseNameEntry.Text;

            expense.Category = categoryPicker.SelectedItem?.ToString();
            expense.CreationDateTime = DateTime.Now;
            expense.ModificationDateTime = DateTime.Now;
            expense.Id = Guid.NewGuid().ToString();

            MessagingCenter.Send(this, "AddExpense2", expense);

            expenseQntEntry.Text = "";
            expenseNameEntry.Text = "";
            categoryPicker.SelectedItem = null;
        }

        private void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            
        }

    }
}