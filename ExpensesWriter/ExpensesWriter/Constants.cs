using System;
using System.Collections.Generic;
using System.Text;

namespace ExpensesWriter
{
    public class Constants
    {
        ////////for WebAPI service debug and development
        public static string BaseApiAddress => "http://192.168.88.171:44376/";
        //public static string BaseApiAddress => "https://expenseswriter.azurewebsites.net/";

        public static string ListenConnectionString => "Endpoint=sb://sergiynotificationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=HL9tpugxzELLM7bz/EH1XqE/uxuGHK+i6X2/jPNVCFY=";
        public static string NotificationHubName => "XamarinAndroidNotificationsHub";
    }
}
