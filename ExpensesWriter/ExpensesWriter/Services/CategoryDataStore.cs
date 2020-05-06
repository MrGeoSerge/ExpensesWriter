using ExpensesWriter.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesWriter.Services
{
    public class CategoriesDataStore// : IDataStore<Category>
    {
        IEnumerable<BudgetItem> expenses;
        readonly SQLiteAsyncConnection database;

        public CategoriesDataStore()
        {
            string path = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "ExpensesSQLite.db3");
            database = new SQLiteAsyncConnection(path);
            database.CreateTableAsync<BudgetItem>().Wait();
        }

        public async Task<IEnumerable<BudgetItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await database.Table<BudgetItem>().ToListAsync();
        }


        public async Task<BudgetItem> GetItemAsync(int id)
        {
            return await database.Table<BudgetItem>()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> AddItemAsync(BudgetItem category)
        {
            await database.InsertAsync(category);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemsAsync(string id)
        {
            await database.DeleteAsync<BudgetItem>(id);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(BudgetItem category)
        {
            await database.UpdateAsync(category);
            return await Task.FromResult(true);
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
