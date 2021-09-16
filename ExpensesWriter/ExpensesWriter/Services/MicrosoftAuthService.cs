using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;
using Xamarin.Essentials;

namespace ExpensesWriter.Services
{
    public class MicrosoftAuthService
    {
        private readonly string ClientID = "91d6c232-548c-44bf-8d28-71249d83763d";
        private readonly string[] Scopes = {"User.Read"};
        private readonly string GraphUrl = "https://graph.microsoft.com/";
        private readonly string AppId = "com.sergiy.expensesWriter";
        private readonly string AndroidSignatureHash = "";

        private IPublicClientApplication publicClientApplication;

        string RedirectUri
        {
            get
            {
                if (DeviceInfo.Platform == DevicePlatform.Android)
                    return $"msauth://{AppId}/{{YOUR_SIGNATURE_HASH}}";
                else if (DeviceInfo.Platform == DevicePlatform.iOS)
                    return $"msauth.{AppId}://auth";

                return string.Empty;
            }
        }


        public MicrosoftAuthService()
        {
            this.publicClientApplication = PublicClientApplicationBuilder.Create(ClientID)
                .WithIosKeychainSecurityGroup(AppId)
                .WithRedirectUri(RedirectUri)
                .WithAuthority("https://login.microsoftonline.com/consumers")
                .Build();
        }

        public static object ParentWindow { get; set; }

        public async Task<bool> SignInAsync()
        {
            try
            {
                Debug.WriteLine("Hello SignInAsync ...");
                var accounts = await publicClientApplication.GetAccountsAsync();
                var firstAccount = accounts.FirstOrDefault();
                var authResult = await publicClientApplication.AcquireTokenSilent(Scopes, firstAccount).ExecuteAsync();

                Debug.WriteLine("Hello auth ...");
                Debug.WriteLine(authResult?.AccessToken);

                // Store the access token securely for later use.
                await SecureStorage.SetAsync("AccessToken", authResult?.AccessToken);

                return true;
            }
            catch (MsalUiRequiredException)
            {
                try
                {
                    // This means we need to login again through the MSAL window.
                    var authResult = await publicClientApplication.AcquireTokenInteractive(Scopes)
                        .WithParentActivityOrWindow(ParentWindow)
                        .ExecuteAsync();

                    // Store the access token securely for later use.
                    await SecureStorage.SetAsync("AccessToken", authResult?.AccessToken);

                    return true;
                }
                catch (Exception ex2)
                {
                    Debug.WriteLine(ex2.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }

        }


        public async Task<bool> SignOutAsync()
        {
            try
            {
                var accounts = await publicClientApplication.GetAccountsAsync();

                // Go through all accounts and remove them.
                while (accounts.Any())
                {
                    await publicClientApplication.RemoveAsync(accounts.FirstOrDefault());
                    accounts = await publicClientApplication.GetAccountsAsync();
                }

                // Clear our access token from secure storage.
                SecureStorage.Remove("AccessToken");

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }

    }
}
