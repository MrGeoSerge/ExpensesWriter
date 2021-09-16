using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpensesWriter.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpensesWriter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SSOLoginPage : ContentPage
    {
        public SSOLoginPage()
        {
            InitializeComponent();
            BindingContext = new SSOLoginViewModel();
        }
    }
}