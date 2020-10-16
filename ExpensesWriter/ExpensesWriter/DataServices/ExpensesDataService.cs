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
using System.Diagnostics;
using ExpensesWriter.Views;
using System.Globalization;
using System.Linq;

namespace ExpensesWriter.Services
{
    public class ExpensesDataService
    {
        HttpClient client;
        IEnumerable<Expense> expenses;

        public ExpensesDataService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);

            client.BaseAddress = new Uri($"{App.AzureBackendUrl}");

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
                    var json = await client.GetStringAsync($"api/CurMonthExpenses");
                    expenses = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Expense>>(json));
                }

                return expenses;
            }
            catch(HttpRequestException ex)
            {
                if (ex.Message.Contains("401"))
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("Authorization Error", "Authorization failed. Please login into application", "Got it");
                        Application.Current.MainPage = new NavigationPage(new LoginPage());
                    });
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("GetCurrentMonthItemsAsyncError", ex.ToString(), "Got it");
                    });
                }
            }
            catch(Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("GetCurrentMonthItemsAsyncError", ex.ToString(), "Got it");
                });
            }
                return null;
        }

        public async Task<Expense> GetLastModifiedItemAsync()
        {
            try
            {
                Expense expense = null;
                if (IsConnected)
                {
                    var json = await client.GetStringAsync($"api/LastModifiedExpense");
                    expense = await Task.Run(() => JsonConvert.DeserializeObject<Expense>(json));
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("Connection Error", "No Internet Connection", "Got it");
                    });
                }

                return expense;
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

        public async Task<IEnumerable<Expense>> GetModifiedItemsAsync(DateTime modifiedDateTime)
        {
            try
            {
                if (IsConnected)
                {
                    ///TODO: think of date formats in different cultures
                    string lastModified = modifiedDateTime.Ticks.ToString();
                    client.DefaultRequestHeaders.Add("LastModified", lastModified);
                    var json = await client.GetStringAsync($"api/ModifiedExpenses");
                    expenses = JsonConvert.DeserializeObject<IEnumerable<Expense>>(json);
                }
                else
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage.DisplayAlert("Connection Error", "No Internet Connection", "Got it");
                    });
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


        public async Task<IEnumerable<Expense>> GetLastMonthItemsAsync(bool forceRefresh = false)
        {
            try
            {
                if (forceRefresh && IsConnected)
                {
                    var json = await client.GetStringAsync($"api/LastMonthExpenses");
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

        public async Task<IEnumerable<Expense>> GetFamilyCurrentMonthItemsAsync(bool forceRefresh = false)
        {
            try
            {
                if (forceRefresh && IsConnected)
                {
                    var json = await client.GetStringAsync($"api/FamilyCurrentMonthExpenses");
                    expenses = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Expense>>(json));
                }

                return expenses;
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage.DisplayAlert("GetFamilyCurrentMonthItemsAsyncError", ex.ToString(), "Got it");
                });
                return null;
            }
        }

        public async Task<IEnumerable<Expense>> GetFamilyLastMonthItemsAsync(bool forceRefresh = false)
        {
            try
            {
                if (forceRefresh && IsConnected)
                {
                    var json = await client.GetStringAsync($"api/FamilyLastMonthExpenses");
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
            try
            {
                if (expense == null || expense.Id == null || !IsConnected)
                    return false;

                var budgetItem = expense.BudgetItem;
                expense.BudgetItem = null;

                var serializedExpense = JsonConvert.SerializeObject(expense);
                var response = await client.PutAsync($"api/expenses/put", new StringContent(serializedExpense, Encoding.UTF8, "application/json"));

                expense.BudgetItem = budgetItem;
                return response.IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
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
