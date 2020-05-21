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
