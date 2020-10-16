using ExpensesWriter.Models;
using ExpensesWriter.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class CategoryEditViewModel : BaseViewModel
    {
        public BudgetItem Category { get; set; }



        public ICommand SaveCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await new BudgetItemsDataService().AddItemAsync(Category);
                    
                    Device.BeginInvokeOnMainThread(async () => 
                            await App.Current.MainPage.Navigation.PopModalAsync());
                });
            }
        }
        public CategoryEditViewModel(BudgetItem category = null)
        {
            Title = category?.Name;
            Category = category;
        }

        public CategoryEditViewModel():this(null)
        {
            Category = new BudgetItem();
        }
    }
}
