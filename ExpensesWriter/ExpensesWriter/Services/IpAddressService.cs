using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ExpensesWriter.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace ExpensesWriter.Services
{
    public class IpAddressService
    {
        HttpClient client;

        public IpAddressService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}");

        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;


        public async Task<string> GetIpAddress()
        {
            if (IsConnected)
            {
                var ipData = await client.GetStringAsync($"api/expenses");
                //expenses = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Expense>>(json));
                return ipData;
            }

            return string.Empty;
        }

        
    }
}
