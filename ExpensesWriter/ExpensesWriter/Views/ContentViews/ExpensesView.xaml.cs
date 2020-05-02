using ExpensesWriter.Models;
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

    }
}