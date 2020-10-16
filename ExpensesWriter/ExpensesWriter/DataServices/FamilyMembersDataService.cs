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
    public class FamilyMembersDataService
    {
        HttpClient client;
        IEnumerable<FamilyMember> items;

        public FamilyMembersDataService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);

            client.BaseAddress = new Uri($"{App.AzureBackendUrl}");

            items = new List<FamilyMember>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<IEnumerable<FamilyMember>> GetFamilyMembersAsync()
        {
            if (IsConnected)
            {
                var json = await client.GetStringAsync($"api/FamilyMembers");
                items = JsonConvert.DeserializeObject<IEnumerable<FamilyMember>>(json);
            }
            return items;
        }

    }
}
