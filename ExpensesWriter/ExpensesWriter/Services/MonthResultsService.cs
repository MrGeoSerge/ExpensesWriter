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
        public CategoriesMockDataStore CategoriesDataStore => new CategoriesMockDataStore();

        public async Task<ObservableCollection<CategoryExpense>> GetCurrentMonthResults()
        {
            var expenses = await DataStore.GetCurrentMonthItemsAsync(true);

            return await GetCategorizedMonthResults(expenses);
        }

        public async Task<ObservableCollection<CategoryExpense>> GetFamilyCurrentMonthResults()
        {
            var expenses = await DataStore.GetFamilyCurrentMonthItemsAsync(true);

            return await GetCategorizedMonthResults(expenses);
        }

        public async Task<ObservableCollection<CategoryExpense>> GetFamilyLastMonthResults()
        {
            var expenses = await DataStore.GetFamilyLastMonthItemsAsync(true);

            return await GetCategorizedMonthResults(expenses);
        }

        public async Task<ObservableCollection<CategoryExpense>> GetLastMonthResults()
        {
            var expenses = await DataStore.GetLastMonthItemsAsync(true);

            return await GetCategorizedMonthResults(expenses);
        }

        protected async Task<ObservableCollection<CategoryExpense>> GetCategorizedMonthResults(IEnumerable<Expense> expenses)
        {
            var categories = await CategoriesDataStore.GetItemsAsync(true);

            List<string> categoryStrings = categories.Select(category => category.Name).ToList();

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
