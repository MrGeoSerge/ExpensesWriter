using Android.Util;
using WindowsAzure.Messaging;
using Firebase.Iid;
using Android.App;
using System.Collections.Generic;

using ExpensesWriter.Helpers;


namespace ExpensesWriter.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        NotificationHub hub;

        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "FCM token: " + refreshedToken);
        }






    }
}