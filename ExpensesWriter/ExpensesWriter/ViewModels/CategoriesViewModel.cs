using ExpensesWriter.Models;
using ExpensesWriter.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        public ObservableCollection<BudgetItem> categories;
        public ObservableCollection<BudgetItem> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }


        public Command LoadCategoriesCommand { get; set; }

        public CategoriesViewModel()
        {
            Title = "Expenses Categories";
            Categories = new ObservableCollection<BudgetItem>();
            LoadCategoriesCommand = new Command(async () => await ExecuteLoadCategoriesCommand());
            LoadCategoriesCommand.Execute(null);
        }

        private async Task ExecuteLoadCategoriesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Categories.Clear();
                var categories = await GetCategories();
                //var sortedExpenses = expenses.Cast<Expense>().OrderByDescending((x) => x.CreationDateTime).Select(x => x);
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<IEnumerable<BudgetItem>> GetCategories()
        {
            return await new CategoriesAzureDataStore().GetItemsAsync(true);
            //return await CategoriesDataStore.GetItemsAsync();
        }
    }
}
