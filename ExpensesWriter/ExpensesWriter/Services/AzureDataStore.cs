using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using ExpensesWriter.Models;
using System.Net.Http.Headers;
using ExpensesWriter.Helpers;
using Xamarin.Forms;

namespace ExpensesWriter.Services
{
    public class AzureDataStore : IDataStore<Expense>
    {
        HttpClient client;
        IEnumerable<Expense> expenses;

        public AzureDataStore()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);

            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            expenses = new List<Expense>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Expense>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/expenses");
                expenses = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Expense>>(json));
            }

            return expenses;
        }

        public async Task<IEnumerable<Expense>> GetCurrentMonthItemsAsync(bool forceRefresh = false)
        {
            try
            {
                if (forceRefresh && IsConnected)
                {
                    var json = await client.GetStringAsync($"api/Curmonthexpenses");
                    expenses = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Expense>>(json));
                }

                return expenses;
            }
            catch(Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("GetCurrentMonthItemsAsyncError", ex.ToString(), "Got it");
                });
                return null;
            }
        }

        public async Task<IEnumerable<Expense>> GetPreviousMonthItemsAsync(bool forceRefresh = false)
        {
            try
            {
                if (forceRefresh && IsConnected)
                {
                    var json = await client.GetStringAsync($"api/PreviousMonthExpenses");
                    expenses = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Expense>>(json));
                }

                return expenses;
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("GetCurrentMonthItemsAsyncError", ex.ToString(), "Got it");
                });
                return null;
            }
        }


        public async Task<Expense> GetItemAsync(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/expenses/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Expense>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Expense expense)
        {
            if (expense == null || !IsConnected)
                return false;

            var serializedExpense = JsonConvert.SerializeObject(expense);

            var response = await client.PostAsync($"api/expenses", new StringContent(serializedExpense, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Expense expense)
        {
            if (expense == null || expense.Id == null || !IsConnected)
                return false;

            expense.ModificationDateTime = DateTime.UtcNow;

            var serializedExpense = JsonConvert.SerializeObject(expense);
            var response = await client.PutAsync($"api/expenses/put", new StringContent(serializedExpense, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemsAsync(string id)
        {
            if (string.IsNullOrEmpty(id) && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/expenses/{id}");

            return response.IsSuccessStatusCode;
        }

    }
}
