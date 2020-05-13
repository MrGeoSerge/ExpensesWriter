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
using System.Collections.ObjectModel;

namespace ExpensesWriter.Services
{
    public class BudgetPlanningItemsAzureDataStore
    {
        HttpClient client;
        IEnumerable<BudgetPlanningItem> items;

        public BudgetPlanningItemsAzureDataStore()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);

            client.BaseAddress = new Uri($"{App.AzureBackendUrl}");

            items = new List<BudgetPlanningItem>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<BudgetPlanningItem>> GetCurrentMonthItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/CurMonthBudgetPlanningItems").ConfigureAwait(false);
                items = JsonConvert.DeserializeObject<IEnumerable<BudgetPlanningItem>>(json);
            }

            return items;
        }

        public async Task<bool> UpdateItemAsync(BudgetPlanningItem item)
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PutAsync($"api/UpdatePlanningItem", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateMonthItemsAsync(ObservableCollection<BudgetPlanningItem> items)
        {
            if (items == null || !IsConnected || items.Count == 0)
                return false;

            var serializedItem = JsonConvert.SerializeObject(items);

            var response = await client.PutAsync($"api/UpdateMonthPlanningItems", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }
    }
}
