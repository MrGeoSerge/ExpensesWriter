using ExpensesWriter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class BaseExpenseViewModel : BaseViewModel
    {

        //public Command LoadCategoriesCommand { get; set; }

        public BaseExpenseViewModel()
        {
            //Categories = new ObservableCollection<BudgetItem>();
            //CategoriesString = new ObservableCollection<string>();
            //LoadCategoriesCommand = new Command(async () => await ExecuteLoadCategoriesCommand());
            //LoadCategoriesCommand.Execute(null);
            //ExecuteLoadCategoriesCommand().ConfigureAwait(false).GetAwaiter();
        }

        //private async Task ExecuteLoadCategoriesCommand()
        //{
        //    //if (IsBusy)
        //    //    return;

        //    //IsBusy = true;

        //    try
        //    {
        //        Categories.Clear();
        //        var categories = await CategoriesDataStore.GetItemsAsync(true).ConfigureAwait(true);
        //        var x = 0;
        //        foreach (var category in categories)
        //        {
        //            Categories.Add(category);
        //            CategoriesString.Add(category.Name);
        //        }
        //        CategoryDefaultString = CategoriesString[0];
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}


    }
}
