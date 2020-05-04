using ExpensesWriter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpensesWriter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCategoryPage : ContentPage
    {
        public NewCategoryPage()
        {
            InitializeComponent();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            //MessagingCenter.Send(this, "AddExpense", Expense);

            //await new CategoriesAzureDataStore().AddItemAsync(viewMo)
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

    }
}