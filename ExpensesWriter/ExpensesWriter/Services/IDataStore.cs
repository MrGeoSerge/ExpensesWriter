using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpensesWriter.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T expense);
        Task<bool> UpdateItemAsync(T expense);
        Task<bool> DeleteItemsAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<IEnumerable<T>> GetCurrentMonthItemsAsync(bool forceRefresh = false);
    }
}
