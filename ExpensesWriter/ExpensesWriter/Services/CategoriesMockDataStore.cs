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
        List<Category> categories;

        public CategoriesMockDataStore()
        {
            categories = new List<Category>
            {
                //new Category { Id = Guid.NewGuid().ToString(), Name ="default" },
                new Category { Id = 1, Name ="default" },
                new Category { Id = 2, Name ="food home" },//
                new Category { Id = 3, Name ="food work" },//
                new Category { Id = 4, Name ="bills" },//
                new Category { Id = 5, Name ="car" },//
                new Category { Id = 6, Name ="entertainment" },//
                new Category { Id = 7, Name ="parents" },
                new Category { Id = 8, Name ="work" },
                new Category { Id = 9, Name ="education" },
                new Category { Id = 10, Name ="cats" },
                new Category { Id = 11, Name ="house equipment" },
                new Category { Id = 12, Name ="health" },
                new Category { Id = 13, Name ="clothes" },
                new Category { Id = 14, Name ="travels" },
                new Category { Id = 15, Name ="gifts" },//
                new Category { Id = 16, Name ="country house" },
                new Category { Id = 17, Name ="other" },
                new Category { Id = 18, Name ="currency put off" }
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

        public async Task<bool> DeleteItemsAsync(int id)
        {
            var oldExpense = categories.Where((Category arg) => arg.Id == id).FirstOrDefault();
            categories.Remove(oldExpense);

            return await Task.FromResult(true);
        }

        public async Task<Category> GetItemAsync(int id)
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

        public Task<IEnumerable<Category>> GetLastMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetFamilyCurrentMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Category>> GetFamilyLastMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }
    }
}
