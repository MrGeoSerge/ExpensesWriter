using ExpensesWriter.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesWriter.Services
{
    public class CategoriesDataStore : IDataStore<Category>
    {
        IEnumerable<Category> expenses;
        readonly SQLiteAsyncConnection database;

        public CategoriesDataStore()
        {
            string path = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "ExpensesSQLite.db3");
            database = new SQLiteAsyncConnection(path);
            database.CreateTableAsync<Category>().Wait();
        }

        public async Task<IEnumerable<Category>> GetItemsAsync(bool forceRefresh = false)
        {
            return await database.Table<Category>().ToListAsync();
        }


        public async Task<Category> GetItemAsync(string id)
        {
            return await database.Table<Category>()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> AddItemAsync(Category category)
        {
            await database.InsertAsync(category);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemsAsync(string id)
        {
            await database.DeleteAsync<Category>(id);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Category category)
        {
            await database.UpdateAsync(category);
            return await Task.FromResult(true);
        }

        public Task<IEnumerable<Category>> GetCurrentMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }
    }
}
