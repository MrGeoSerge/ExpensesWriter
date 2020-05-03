using ExpensesWriter.ViewModels;
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
    public partial class CurrentMonthResultsPage : ContentPage
    {
        public CurrentMonthResultsPage()
        {
            BindingContext = new MonthResultsViewModel();
            InitializeComponent();

        }
    }
}