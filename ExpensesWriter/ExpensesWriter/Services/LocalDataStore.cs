using ExpensesWriter.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesWriter.Services
{
    public class LocalDataStore : IDataStore<Expense>
    {
        IEnumerable<Expense> expenses;
        readonly SQLiteAsyncConnection database;

        public LocalDataStore()
        {
            string path = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "ExpensesSQLite.db3");
            database = new SQLiteAsyncConnection(path);
            database.CreateTableAsync<Expense>().Wait();
        }

        public async Task<IEnumerable<Expense>> GetItemsAsync(bool forceRefresh = false)
        {
            return await database.Table<Expense>().ToListAsync();
        }


        public async Task<Expense> GetItemAsync(string id)
        {
            return await database.Table<Expense>()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> AddItemAsync(Expense expense)
        {
             await database.InsertAsync(expense);
             return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemsAsync(string id)
        {
            await database.DeleteAsync<Expense>(id);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Expense expense)
        {
            await database.UpdateAsync(expense);
            return await Task.FromResult(true);
        }

        public Task<IEnumerable<Expense>> GetCurrentMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Expense>> GetPreviousMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Expense>> GetFamilyCurrentMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Expense>> GetFamilyPreviousMonthItemsAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }
    }
}
