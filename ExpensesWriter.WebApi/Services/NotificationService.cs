using PushSharp.Apple;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using PushSharp.Google;
using Newtonsoft.Json;

namespace ExpensesWriter.WebApi.Services
{
    public class NotificationService
    {

        public void SendAndroidNotification(string deviceToken, string message)
        {
            
            GcmConfiguration configuration = new GcmConfiguration("AAAAByfoliw:APA91bH3Av2zEoMHQsQiSphktNSxOnyyU4hC2sT7A9PBA8iAM0I-qt527Cx7vdOM-8tAGmd2t8oJVJ1bX3kELi-IDmO1wX2AgUbb_miqR6m6TR6-DbSJEf_rgKI68moqhHYAgWymWCx8");
            configuration.GcmUrl = "https://fcm.googleapis.com/fcm/send";
            var gcmBroker = new GcmServiceBroker(configuration);

            GcmNotification notif = new GcmNotification();
            notif.Notification = JObject.Parse("{ \"data\" : {\"message\":\"" + "From " + "SergeUser" + ": " + message + "\"}}");
            notif.RegistrationIds = new List<string>()
            {
                deviceToken
            };

            // Wire up events
            gcmBroker.OnNotificationFailed += (notification, aggregateEx) => {

                aggregateEx.Handle(ex => {

                    // See what kind of exception it was to further diagnose
                    if (ex is ApnsNotificationException notificationException)
                    {

                        // Deal with the failed notification
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;

                        Trace.WriteLine($"Android Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}");

                    }
                    else
                    {
                        // Inner exception might hold more useful information like an ApnsConnectionException			
                        Console.WriteLine($"Android Notification Failed for some unknown reason : {ex.InnerException}");
                    }

                    // Mark it as handled
                    return true;
                });
            };


            gcmBroker.OnNotificationSucceeded += (notification) => {
                Trace.WriteLine("Android Notification Sent!");
            };

            gcmBroker.Start();

            gcmBroker.QueueNotification(notif);

            gcmBroker.Stop();
            
            
            //.ForDeviceRegistrationId(deviceToken).WithJson(JsonConvert.SerializeObject(message));
            //_push.QueueNotification(notif);
        }










        public void SendAppleNotification(string deviceToken, string message)
        {
            ////TODO: make relative path
            string pathToCert = @"D:\MyApps\ExpensesWriter\ExpensesWriter.WebApi\Certificates\sergiyExpensesWriter_sandboxCertificate.p12";

            var config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Sandbox, pathToCert, "se15rge", true);

            var apnsBroker = new ApnsServiceBroker(config);

            // Wire up events
            apnsBroker.OnNotificationFailed += (notification, aggregateEx) => {

                aggregateEx.Handle(ex => {

                    // See what kind of exception it was to further diagnose
                    if (ex is ApnsNotificationException notificationException)
                    {

                        // Deal with the failed notification
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;

                        Trace.WriteLine($"Apple Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}");

                    }
                    else
                    {
                        // Inner exception might hold more useful information like an ApnsConnectionException			
                        Console.WriteLine($"Apple Notification Failed for some unknown reason : {ex.InnerException}");
                    }

                    // Mark it as handled
                    return true;
                });
            };

            apnsBroker.OnNotificationSucceeded += (notification) => {
                Trace.WriteLine("Apple Notification Sent!");
            };

            // Start the broker
            apnsBroker.Start();

            //foreach (var deviceToken in MY_DEVICE_TOKENS)
            //{
                // Queue a notification to send
                apnsBroker.QueueNotification(new ApnsNotification
                {
                    DeviceToken = deviceToken,
                    Payload = JObject.Parse("{ \"aps\" : { \"alert\" : \"" + message + "\", \"badge\" : 1, \"sound\" : \"default\" } }")
                    
                });
            //}

            // Stop the broker, wait for it to finish   
            // This isn't done after every message, but after you're
            // done with the broker
            apnsBroker.Stop();


        }
    }
}