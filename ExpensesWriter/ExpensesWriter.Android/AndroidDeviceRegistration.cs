using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExpensesWriter.Models;
using Firebase.Iid;

namespace ExpensesWriter.Droid
{
    public class AndroidDeviceRegistration : IDeviceRegistration
    {
        public DeviceRegistration GetDeviceInfo()
        {
            DeviceRegistration device = new DeviceRegistration();
            device.DeviceToken = FirebaseInstanceId.Instance.Token;
            device.Platform = "Android";
            return device;
        }
    }
}