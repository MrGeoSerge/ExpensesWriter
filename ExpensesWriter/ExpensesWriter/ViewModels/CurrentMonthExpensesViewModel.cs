using ExpensesWriter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class CurrentMonthExpensesViewModel : ExpensesViewModel
    {
        public ObservableCollection<BudgetItem> Categories { get; set; } = new ObservableCollection<BudgetItem>();

        public ObservableCollection<string> CategoriesString { get; set; } = new ObservableCollection<string>();

        private string categoryDefaultString;
        public string CategoryDefaultString 
        {
            get
            {
                return categoryDefaultString;
            }
            set 
            {
                categoryDefaultString = value;
                OnPropertyChanged();
            } 
        }


        public CurrentMonthExpensesViewModel()
        {
            Title = "Current Month Expenses";



        }

        protected override async Task ExecuteLoadExpensesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Expenses.Clear();
                var expenses = await GetExpenses();
                var sortedExpenses = expenses.Cast<Expense>().OrderByDescending((x) => x.CreationDateTime).Select(x => x);
                foreach (var expense in sortedExpenses)
                {
                    Expenses.Add(expense);
                }
                
                //Categories = new ObservableCollection<BudgetItem>();
                //CategoriesString = new ObservableCollection<string>();

                Categories.Clear();
                var categories = await CategoriesDataStore.GetItemsAsync(true).ConfigureAwait(true);
                var x123 = 0;
                foreach (var category in categories)
                {
                    Categories.Add(category);
                    CategoriesString.Add(category.Name);
                }
                App.CategoriesList = CategoriesString;
                App.BudgetItems = Categories;
                CategoryDefaultString = CategoriesString[0];


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                //throw;
            }
            finally
            {
                IsBusy = false;
            }
        }





        protected override async Task<IEnumerable<Expense>> GetExpenses()
        {
            return await DataStore.GetCurrentMonthItemsAsync(true);
        }

    }
}
