using ExpensesWriter.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesWriter.Repositories.Local
{
    public class ExpensesDataStore
    {
        readonly SQLiteAsyncConnection database;

        public ExpensesDataStore()
        {
            string path = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "ExpensesSQLite.db3");
            database = new SQLiteAsyncConnection(path);
            database.CreateTableAsync<Expense>().Wait();
            database.CreateTableAsync<BudgetItem>().Wait();
        }

        public async Task<IEnumerable<Expense>> GetItemsAsync(bool forceRefresh = false)
        {
            var items = await database.Table<Expense>().ToListAsync();
            var items2 = await database.GetAllWithChildrenAsync<Expense>();
            return items2;
        }

        public async Task AddItemsAsync(IEnumerable<Expense> expenses)
        {
            await database.InsertAllWithChildrenAsync(expenses);
        }

        public async Task<Expense> GetItemAsync(string id)
        {
            return await database.Table<Expense>()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<int> AddItemAsync(Expense expense)
        {
            return await database.InsertAsync(expense);
        }

        public async Task<int> DeleteItemAsync(string id)
        {
            return await database.DeleteAsync<Expense>(id);
        }

        public async Task<Expense> GetLastModifiedItemAsync()
        {
            Expense item = await database.Table<Expense>().OrderByDescending(x => x.ModificationDateTime).FirstOrDefaultAsync();
            return item;
        }

        public async Task<int> UpdateItemAsync(Expense expense)
        {
            return await database.UpdateAsync(expense);
        }
    }
}
