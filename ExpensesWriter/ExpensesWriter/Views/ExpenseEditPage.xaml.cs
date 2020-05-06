using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ExpensesWriter.Models;
using ExpensesWriter.ViewModels;
using ExpensesWriter.Services;

namespace ExpensesWriter.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ExpenseEditPage : ContentPage
    {
        ExpenseEditViewModel viewModel;

        public ExpenseEditPage(ExpenseEditViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        protected override void OnDisappearing()
        {
            
            base.OnDisappearing();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            //IDataStore<Expense> DataStore = DependencyService.Get<IDataStore<Expense>>();
            //await DataStore.UpdateItemAsync(viewModel.Expense);

            await Navigation.PopAsync();

        }
    }
}