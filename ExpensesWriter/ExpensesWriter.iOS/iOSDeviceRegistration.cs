using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpensesWriter.Models;
using Foundation;
using UIKit;

namespace ExpensesWriter.iOS
{
    public class IosDeviceRegistration : IDeviceRegistration
    {
        public DeviceRegistration GetDeviceInfo()
        {
            DeviceRegistration deviceRegistration = new DeviceRegistration();
            deviceRegistration.DeviceToken = App.AppleDeviceToken;
            deviceRegistration.Platform = "iOS";
            return deviceRegistration;
        }
    }
}