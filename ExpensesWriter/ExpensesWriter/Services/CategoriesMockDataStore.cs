using ExpensesWriter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ExpensesWriter.Services
{
    public class CategoriesMockDataStore// : IDataStore<Category>
    {
        List<BudgetItem> categories;

        public CategoriesMockDataStore()
        {
            categories = new List<BudgetItem>
            {
                //new Category { Id = Guid.NewGuid().ToString(), Name ="default" },
                new BudgetItem { Id = 1, Name ="default" },
                new BudgetItem { Id = 2, Name ="food home" },//
                new BudgetItem { Id = 3, Name ="food work" },//
                new BudgetItem { Id = 4, Name ="bills" },//
                new BudgetItem { Id = 5, Name ="car" },//
                new BudgetItem { Id = 6, Name ="entertainment" },//
                new BudgetItem { Id = 7, Name ="parents" },
                new BudgetItem { Id = 8, Name ="work" },
                new BudgetItem { Id = 9, Name ="education" },
                new BudgetItem { Id = 10, Name ="cats" },
                new BudgetItem { Id = 11, Name ="house equipment" },
                new BudgetItem { Id = 12, Name ="health" },
                new BudgetItem { Id = 13, Name ="clothes" },
                new BudgetItem { Id = 14, Name ="travels" },
                new BudgetItem { Id = 15, Name ="gifts" },//
                new BudgetItem { Id = 16, Name ="country house" },
                new BudgetItem { Id = 17, Name ="other" },
                new BudgetItem { Id = 18, Name ="currency put off" }
            };

        }

        public async Task<bool> AddItemAsync(BudgetItem category)
        {
            categories.Add(category);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(BudgetItem category)
        {
            var oldExpense = categories.Where((BudgetItem arg) => arg.Id == category.Id).FirstOrDefault();
            categories.Remove(oldExpense);
            categories.Add(category);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemsAsync(int id)
        {
            var oldExpense = categories.Where((BudgetItem arg) => arg.Id == id).FirstOrDefault();
            categories.Remove(oldExpense);

            return await Task.FromResult(true);
        }

        public async Task<BudgetItem> GetItemAsync(int id)
        {
            return await Task.FromResult(categories.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<BudgetItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(categories);
        }

        public Task<IEnumerable<BudgetItem>> GetCurrentMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BudgetItem>> GetLastMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BudgetItem>> GetFamilyCurrentMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BudgetItem>> GetFamilyLastMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }
    }
}
