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
            var items = await database.GetAllWithChildrenAsync<Expense>();
            return items;
        }

        public async Task AddItemsAsync(IEnumerable<Expense> expenses)
        {
            foreach(var expense in expenses)
            {
                if(await GetItemAsync(expense.Id) == null)
                {
                    await database.InsertWithChildrenAsync(expense);
                }
                else
                {
                    await database.UpdateWithChildrenAsync(expense);
                }
            }
        }

        public async Task<Expense> GetItemAsync(string id)
        {
            return await database.Table<Expense>()
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddItemAsync(Expense expense)
        {
            await database.InsertWithChildrenAsync(expense);
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

        public async Task UpdateItemAsync(Expense expense)
        {
            await database.UpdateWithChildrenAsync(expense);
        }
    }
}
