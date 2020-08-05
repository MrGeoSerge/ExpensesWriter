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
    public partial class FamilyNextMonthPlanningPage : ContentPage
    {
        public FamilyNextMonthPlanningPage()
        {
            BindingContext = new FamilyNextMonthPlanningViewModel();
            InitializeComponent();
        }
    }
}