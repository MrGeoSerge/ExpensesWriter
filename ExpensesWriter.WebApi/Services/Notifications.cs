using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

using Microsoft.Azure.NotificationHubs;

namespace ExpensesWriter.WebApi.Services
{
    public class Notifications
    {
        public static Notifications Instance = new Notifications();

        public NotificationHubClient Hub { get; set; }

        private Notifications()
        {
            Hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://sergiynotificationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=cgOfHGF9DFxlZltcgJMoKauSiJfBscx9T7BnnBwZmRg=",
                                                                            "XamarinAndroidNotificationsHub");
        }
    }
}