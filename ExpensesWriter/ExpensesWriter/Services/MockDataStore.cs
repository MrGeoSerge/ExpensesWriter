using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpensesWriter.Models;

namespace ExpensesWriter.Services
{
    public class MockDataStore : IDataStore<Expense>
    {
        List<Expense> expenses;

        public MockDataStore()
        {
            expenses = new List<Expense>();
            var mockExpenses = new List<Expense>
            {
                new Expense { Id = Guid.NewGuid().ToString(), Money = 123, Name ="This is an expense description." },
                new Expense { Id = Guid.NewGuid().ToString(), Money = 154, Name ="This is an expense description." },
                new Expense { Id = Guid.NewGuid().ToString(), Money = 368, Name ="This is an expense description." },
                new Expense { Id = Guid.NewGuid().ToString(), Money = 545, Name ="This is an expense description." },
                new Expense { Id = Guid.NewGuid().ToString(), Money = 444, Name ="This is an expense description." },
                new Expense { Id = Guid.NewGuid().ToString(), Money = 1256, Name ="This is an expense description." }
            };

            foreach (var expense in mockExpenses)
            {
                expenses.Add(expense);
            }
        }

        public async Task<bool> AddItemAsync(Expense expense)
        {
            expenses.Add(expense);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Expense expense)
        {
            var oldExpense = expenses.Where((Expense arg) => arg.Id == expense.Id).FirstOrDefault();
            expenses.Remove(oldExpense);
            expenses.Add(expense);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemsAsync(string id)
        {
            var oldExpense = expenses.Where((Expense arg) => arg.Id == id).FirstOrDefault();
            expenses.Remove(oldExpense);

            return await Task.FromResult(true);
        }

        public async Task<Expense> GetItemAsync(string id)
        {
            return await Task.FromResult(expenses.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Expense>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(expenses);
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