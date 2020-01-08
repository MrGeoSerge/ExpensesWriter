using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using System.Threading.Tasks;
using Android.App;
using Android.Util;
using Firebase.Iid;
using Microsoft.WindowsAzure.MobileServices;

using ExpensesWriter;

namespace ExpensesWriter.Droid
{
    //[Service]
    //[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    //public class FirebaseRegistrationService : FirebaseInstanceIdService
    //{
    //    const string TAG = "FirebaseRegistrationService";

    //    public override void OnTokenRefresh()
    //    {
    //        var refreshedToken = FirebaseInstanceId.Instance.Token;
    //        Log.Debug(TAG, "Refreshed token: " + refreshedToken);
    //        SendRegistrationTokenToAzureNotificationHub(refreshedToken);
    //    }

    //    void SendRegistrationTokenToAzureNotificationHub(string token)
    //    {
    //        // Update notification hub registration
    //        Task.Run(async () =>
    //        {
    //            var client = new MobileServiceClient(Constants.BaseApiAddress);
    //            var push = client.GetPush();
    //            await AzureNotificationHubService.RegisterAsync(push, token);
    //        });
    //    }
    //}
}