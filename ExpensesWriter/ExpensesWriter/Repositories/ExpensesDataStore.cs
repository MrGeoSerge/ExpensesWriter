using ExpensesWriter.Helpers;
using ExpensesWriter.Models;
using ExpensesWriter.Services;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            var itemsNotDeleted = items.Where(x => x.IsDeleted == false).ToList();
            return itemsNotDeleted;
        }

        public async Task AddItemsAsync(IEnumerable<Expense> expenses)
        {
            foreach(var expense in expenses)
            {
                expense.SentUpdates = true;
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

        public async Task<IEnumerable<Expense>> GetFamilyAllExpenses()
        {
            return await GetItemsAsync();
        }

        public async Task<IEnumerable<Expense>> GetFamilyCurrentMonthExpenses()
        {
            var allExpenses = await GetItemsAsync();
            var familyCurrentMonthExpenses = allExpenses.Where(x => x.CreationDateTime.Year == DateTime.Today.Year && x.CreationDateTime.Month == DateTime.Today.Month);
            return familyCurrentMonthExpenses;
        }

        public async Task<IEnumerable<Expense>> GetFamilyLastMonthExpenses()
        {
            var allExpenses = await GetItemsAsync();

            var lastMonthDate = DateTime.Today.AddMonths(-1);
            var lastMonthYear = lastMonthDate.Year;
            var lastMonthMonth = lastMonthDate.Month;

            var familyLastMonthExpenses = allExpenses.Where(x => x.CreationDateTime.Year == lastMonthYear && x.CreationDateTime.Month == lastMonthMonth);
            return familyLastMonthExpenses;
        }

        public async Task<IEnumerable<Expense>> GetPersonalCurrentMonthExpenses()
        {
            var allExpenses = await GetItemsAsync();
            var userId = await new UserIdService().GetUserIdAsync();
            Debug.WriteLine($"GetPersonalCurrentMonthExpenses for userId {userId}");
            var personalCurrentMonthExpenses = allExpenses.Where(x => x.UserId == userId && x.CreationDateTime.Year == DateTime.Today.Year && x.CreationDateTime.Month == DateTime.Today.Month);
            return personalCurrentMonthExpenses;
        }

        public async Task<IEnumerable<Expense>> GetPersonalUnsentCurrentMonthExpenses()
        {
            var allExpenses = await GetItemsAsync();
            var userId = await new UserIdService().GetUserIdAsync();
            var personalCurrentMonthExpenses = allExpenses
                .Where(x => x.UserId == userId 
                && x.CreationDateTime.Year == DateTime.Today.Year 
                && x.CreationDateTime.Month == DateTime.Today.Month
                && !x.SentUpdates);
            return personalCurrentMonthExpenses;
        }

        public async Task<IEnumerable<Expense>> GetPersonalLastMonthExpenses()
        {
            var allExpenses = await GetItemsAsync();
            var userId = await new UserIdService().GetUserIdAsync();

            var lastMonthDate = DateTime.Today.AddMonths(-1);
            var lastMonthYear = lastMonthDate.Year;
            var lastMonthMonth = lastMonthDate.Month;

            var personalLastMonthExpenses = allExpenses.Where(x => x.UserId == userId && x.CreationDateTime.Year == lastMonthYear && x.CreationDateTime.Month == lastMonthMonth);
            return personalLastMonthExpenses;
        }

    }
}
