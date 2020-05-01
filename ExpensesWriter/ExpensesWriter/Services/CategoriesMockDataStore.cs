using ExpensesWriter.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace ExpensesWriter.Services
{
    public class CategoriesMockDataStore : IDataStore<Category>
    {
        List<Category> categories;

        public CategoriesMockDataStore()
        {
            categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid().ToString(), Name ="default" },
                new Category { Id = Guid.NewGuid().ToString(), Name ="food home" },//
                new Category { Id = Guid.NewGuid().ToString(), Name ="food work" },//
                new Category { Id = Guid.NewGuid().ToString(), Name ="bills" },//
                new Category { Id = Guid.NewGuid().ToString(), Name ="car" },//
                new Category { Id = Guid.NewGuid().ToString(), Name ="entertainment" },//
                new Category { Id = Guid.NewGuid().ToString(), Name ="parents" },
                new Category { Id = Guid.NewGuid().ToString(), Name ="work" },
                new Category { Id = Guid.NewGuid().ToString(), Name ="education" },
                new Category { Id = Guid.NewGuid().ToString(), Name ="cats" },
                new Category { Id = Guid.NewGuid().ToString(), Name ="house equipment" },
                new Category { Id = Guid.NewGuid().ToString(), Name ="health" },
                new Category { Id = Guid.NewGuid().ToString(), Name ="clothes" },
                new Category { Id = Guid.NewGuid().ToString(), Name ="travels" },
                new Category { Id = Guid.NewGuid().ToString(), Name ="gifts" },//
                new Category { Id = Guid.NewGuid().ToString(), Name ="country house" },
                new Category { Id = Guid.NewGuid().ToString(), Name ="other" },
                new Category { Id = Guid.NewGuid().ToString(), Name ="currency put off" }
            };

        }

        public async Task<bool> AddItemAsync(Category category)
        {
            categories.Add(category);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Category category)
        {
            var oldExpense = categories.Where((Category arg) => arg.Id == category.Id).FirstOrDefault();
            categories.Remove(oldExpense);
            categories.Add(category);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemsAsync(string id)
        {
            var oldExpense = categories.Where((Category arg) => arg.Id == id).FirstOrDefault();
            categories.Remove(oldExpense);

            return await Task.FromResult(true);
        }

        public async Task<Category> GetItemAsync(string id)
        {
            return await Task.FromResult(categories.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Category>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(categories);
        }

        public Task<IEnumerable<Category>> GetCurrentMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetPreviousMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }
    }
}
