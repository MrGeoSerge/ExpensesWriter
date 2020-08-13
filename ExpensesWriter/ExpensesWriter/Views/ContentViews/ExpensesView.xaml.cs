using ExpensesWriter.Models;
using ExpensesWriter.Services;
using ExpensesWriter.UpdateServices;
using ExpensesWriter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpensesWriter.Views.ContentViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensesView : ContentView
    {
        public ExpensesView()
        {
            InitializeComponent();
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

        private async void OnDelete(object sender, EventArgs e)
        {
            var expense = ((MenuItem)sender).CommandParameter as Expense;
            RemoveFromListView(expense);
            await new ExpenseService().DeleteExpense(expense);
        }

        //private async Task RemoveFromRemoteStorage(Expense expense)
        //{
        //    var result = await new AzureDataStore().DeleteItemsAsync(expense.Id);
        //    if (!result)
        //        await Application.Current.MainPage.DisplayAlert("Ups", "Item was not deleted. Check your Internet connection please", "Got it");
        //}

        private void RemoveFromListView(Expense expense)
        {
            ContentPage page;
            page = Parent as ContentPage;

            if(page == null)
            {
                page = Parent.Parent as ContentPage;
            }

            if(page != null)
            {
                var viewModel = page.BindingContext as ExpensesViewModel;

                var expenseList = viewModel.Expenses;
                expenseList.Remove(expense);
            }
        }
    }
}