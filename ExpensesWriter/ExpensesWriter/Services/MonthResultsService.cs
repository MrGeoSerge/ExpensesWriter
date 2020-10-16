using ExpensesWriter.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.Services
{
    public class MonthResultsService
    {
        public ExpensesDataService AzureDataStore => new ExpensesDataService();

        public async Task<ObservableCollection<CategoryExpense>> GetCurrentMonthResults()
        {
            var expenses = await AzureDataStore.GetFamilyCurrentMonthItemsAsync(true);

            SetDefaultBudgetItemToExpenseWithNullBudgetItem(ref expenses);


            var categories = GetCategorizedMonthResults(expenses);

            await ApplyBudgetPlanningForCurrentMonth(categories);

            return categories;
        }

        private async Task ApplyBudgetPlanningForCurrentMonth(IEnumerable<CategoryExpense> categories)
        {
            var planningItems = await new BudgetPlanningItemsDataService().GetCurrentMonthItemsAsync(true);

            foreach(var category in categories)
            {
                category.PlannedMoney = planningItems.Where(x => x.BudgetItem.Name == category.Category).Select(x => x.Money).FirstOrDefault();
                category.PercentOfExecution = (int)(category.Money / category.PlannedMoney * 100);
            }
        }

        private async Task ApplyBudgetPlanningForLastMonth(IEnumerable<CategoryExpense> categories)
        {
            var planningItems = await new BudgetPlanningItemsDataService().GetLastMonthItemsAsync(true);

            foreach(var category in categories)
            {
                category.PlannedMoney = planningItems.Where(x => x.BudgetItem.Name == category.Category).Select(x => x.Money).FirstOrDefault();
                category.PercentOfExecution = (int)(category.Money / category.PlannedMoney * 100);
            }
        }

        public async Task<ObservableCollection<CategoryExpense>> GetFamilyCurrentMonthResults()
        {
            var expenses = await AzureDataStore.GetFamilyCurrentMonthItemsAsync(true);
            SetDefaultBudgetItemToExpenseWithNullBudgetItem(ref expenses);
            var categories = GetCategorizedMonthResults(expenses);
            await ApplyBudgetPlanningForCurrentMonth(categories);

            return categories;
        }

        public async Task<ObservableCollection<CategoryExpense>> GetFamilyLastMonthResults()
        {
            var expenses = await AzureDataStore.GetFamilyLastMonthItemsAsync(true);
            SetDefaultBudgetItemToExpenseWithNullBudgetItem(ref expenses);
            var categories = GetCategorizedMonthResults(expenses);
            await ApplyBudgetPlanningForLastMonth(categories);
            return categories;
        }

        public async Task<ObservableCollection<CategoryExpense>> GetLastMonthResults()
        {
            var expenses = await AzureDataStore.GetLastMonthItemsAsync(true);
            SetDefaultBudgetItemToExpenseWithNullBudgetItem(ref expenses);
            var categories = GetCategorizedMonthResults(expenses);
            await ApplyBudgetPlanningForLastMonth(categories);
            return categories;
        }

        protected ObservableCollection<CategoryExpense> GetCategorizedMonthResults(IEnumerable<Expense> expenses)
        {

            //var categories = await CategoriesDataStore.GetItemsAsync(true);
            var categories = App.CategoriesList;

            //List<string> categoryStrings = categories.Select(category => category.Name).ToList();

            try
            {
                ObservableCollection<CategoryExpense> categoryExpenses = new ObservableCollection<CategoryExpense>();

                foreach(var category in categories)
                {

                    var sum = expenses.Where(expense => expense.BudgetItem.Name == category).Sum(expense => expense.Money);

                    CategoryExpense categoryExpense = new CategoryExpense { Category = category, Money = sum };
                    categoryExpenses.Add(categoryExpense);
                }
                return categoryExpenses;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        private void SetDefaultBudgetItemToExpenseWithNullBudgetItem(ref IEnumerable<Expense> expenses)
        {
            foreach(var expense in expenses)
            {
                if(expense.BudgetItem == null)
                {
                    expense.BudgetItem = new BudgetItem { Id = 1, Name = "default" };
                    expense.BudgetItemId = 1;
                }
            }
        }
    }
}
