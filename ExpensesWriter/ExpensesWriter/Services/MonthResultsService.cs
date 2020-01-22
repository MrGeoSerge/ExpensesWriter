using ExpensesWriter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.Services
{
    public class MonthResultsService
    {
        public IDataStore<Expense> DataStore => DependencyService.Get<IDataStore<Expense>>() ?? new MockDataStore();
        public IDataStore<Category> CategoriesDataStore => DependencyService.Get<IDataStore<Category>>() ?? new CategoriesMockDataStore();

        public async Task<ObservableCollection<CategoryExpense>> GetCurrentMonthResults()
        {
            var expenses = await DataStore.GetCurrentMonthItemsAsync(true);


            var categories = await CategoriesDataStore.GetItemsAsync(true);

            List<String> categoryStrings = categories.Select(category => category.Name).ToList();

            ObservableCollection<CategoryExpense> categoryExpenses = new ObservableCollection<CategoryExpense>();

            foreach(var category in categoryStrings)
            {
                var sum = expenses.Where(expense => expense.Category == category).Sum(expense => expense.Money);

                CategoryExpense categoryExpense = new CategoryExpense { Category = category, Money = sum };
                categoryExpenses.Add(categoryExpense);
            }

            return categoryExpenses;
        }
    }
}
