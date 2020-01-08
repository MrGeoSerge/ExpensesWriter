using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ExpensesWriter.Models;
using Xamarin.Forms;
using ExpensesWriter.Helpers;

namespace ExpensesWriter.Services
{
    class RegisterDeviceService
    {
        public async Task<bool> RegisterDeviceAsync()
        {
            DeviceRegistration device = DependencyService.Get<IDeviceRegistration>().GetDeviceInfo();

            var serializedDevice = JsonConvert.SerializeObject(device);



            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            HttpResponseMessage response = await client.PostAsync(
                "api/device", new StringContent(serializedDevice, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
                return true;
            return false;
        }

        public async Task<bool> UnregisterDeviceAsync()
        {
            DeviceRegistration device = DependencyService.Get<IDeviceRegistration>().GetDeviceInfo();
            var serializedDevice = JsonConvert.SerializeObject(device);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            var response = await client.PostAsync(
                "api/device/unregister", new StringContent(serializedDevice, Encoding.UTF8, "application/json"));


            if (response.IsSuccessStatusCode)
                return true;
            return false;

        }
    }
}
