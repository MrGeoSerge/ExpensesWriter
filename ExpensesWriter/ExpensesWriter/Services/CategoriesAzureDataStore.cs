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
    public class CategoriesAzureDataStore
    {
        HttpClient client;
        IEnumerable<BudgetItem> categories;

        public CategoriesAzureDataStore()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);

            client.BaseAddress = new Uri($"{App.AzureBackendUrl}");

            categories = new List<BudgetItem>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<BudgetItem>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/categories").ConfigureAwait(false);
                categories = JsonConvert.DeserializeObject<IEnumerable<BudgetItem>>(json);
            }

            return categories;
        }

        public async Task<bool> AddItemAsync(BudgetItem category)
        {
            if (category == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(category);

            var response = await client.PostAsync($"api/categories", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }



    }
}
