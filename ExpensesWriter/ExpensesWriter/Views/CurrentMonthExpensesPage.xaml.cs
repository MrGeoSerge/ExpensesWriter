using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

using ExpensesWriter.Models;
using ExpensesWriter.ViewModels;
using ExpensesWriter.Helpers;
using Xamarin.Forms.Xaml;

namespace ExpensesWriter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentMonthExpensesPage : ContentPage
    {
        CurrentMonthExpensesViewModel viewModel;
        public CurrentMonthExpensesPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CurrentMonthExpensesViewModel();
        }


        async void AddExpense_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewExpensePage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            expenseQntEntry.Focus();
        }

        private void ExpenseQntEntry_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (e.NewTextValue != string.Empty)
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



            var budgetItem = viewModel.Categories.Where(x => x.Name == categoryPicker.SelectedItem?.ToString()).FirstOrDefault();
            expense.BudgetItemId = budgetItem.Id;
                
                
            //expense.Category = categoryPicker.SelectedItem?.ToString();
            expense.CreationDateTime = DateTime.Now;
            expense.ModificationDateTime = DateTime.Now;
            expense.Id = Guid.NewGuid().ToString();

            MessagingCenter.Send(this, "AddExpense3", expense);

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