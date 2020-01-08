using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.Util;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json.Linq;

namespace ExpensesWriter.Droid
{
    public class AzureNotificationHubService
    {
        //const string TAG = "AzureNotificationHubService";

        //public static async Task RegisterAsync(Push push, string token)
        //{
        //    try
        //    {
        //        const string templateBody = "{\"data\":{\"message\":\"$(messageParam)\"}}";
        //        JObject templates = new JObject();
        //        templates["genericMessage"] = new JObject
        //    {
        //        {"body", templateBody}
        //    };

        //        //await push.RegisterNativeAsync(token, templateBody);

        //        //Registration registration = new Registration();

        //        await push.RegisterNativeAsync(token);
        //        Log.Info("Push Installation Id: ", push.ToString());

        //        Debug.WriteLine($"Push Installation Id: {push.ToString()}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(TAG, "Could not register with Notification Hub: " + ex.Message);
        //        Debug.WriteLine("Could not register with Notification Hub: " + ex.Message);
        //    }
        //}
    }
}