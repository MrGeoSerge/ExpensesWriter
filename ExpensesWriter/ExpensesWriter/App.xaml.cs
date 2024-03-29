﻿using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExpensesWriter.Services;
using ExpensesWriter.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Push;
using ExpensesWriter.Helpers;
using System.Collections.ObjectModel;
using ExpensesWriter.Models;

namespace ExpensesWriter
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        //To debug on Android emulators run the web backend against .NET Core not IIS
        //If using other emulators besides stock Google images you may need to adjust the IP address
        //public static string AzureBackendUrl =
        //    DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://192.168.88.171:44376";
        public static string AzureBackendUrl = Constants.BaseApiAddress;
        //public static bool UseMockDataStore = false;
        //public static bool UseLocalDataStore = false;

        public static string AppleDeviceToken = string.Empty;

        public static ObservableCollection<BudgetItem> BudgetItems = new ObservableCollection<BudgetItem>();
        public static ObservableCollection<string> CategoriesList = new ObservableCollection<string>();

        public App()
        {
            InitializeComponent();

            //if (UseMockDataStore)
            //    DependencyService.Register<MockDataStore>();
            //else if (UseLocalDataStore)
            //    DependencyService.Register<LocalDataStore>();
            //else
            //    DependencyService.Register<AzureDataStore>();

            //DependencyService.Register<CategoriesMockDataStore>();

            SetMainPage();
            //MainPage = new MainPage();
        }

        public void SetMainPage()
        {

            if (!string.IsNullOrEmpty(Settings.Username) && !string.IsNullOrEmpty(Settings.Password))
            {
                if (IsAccessTokenValid())
                {
                    MainPage = new MainPage();
                }
                else
                {
                    MainPage = new NavigationPage(new LoginPage());
                    //MainPage = new NavigationPage(new SSOLoginPage());
                }
            }
            else
            {
                    //MainPage = new NavigationPage(new SSOLoginPage());
                MainPage = new NavigationPage(new RegisterPage());
            }
        }

        private bool IsAccessTokenValid()
        {
            var tokenExpirationDate = Settings.AccessTokenExpirationDate;

            if (tokenExpirationDate > DateTime.UtcNow)
                return true;
            return false;
        }

        protected override void OnStart()
        {
            //AppCenter.Start("ios=8d01f4c7-7839-4c85-ba20-fa0e5221d57d;" +
            //      "uwp={Your UWP App secret here};" +
            //      "android={Your Android App secret here}",
            //      typeof(Analytics), typeof(Crashes));

            //AppCenter.Start("8d01f4c7-7839-4c85-ba20-fa0e5221d57d", typeof(Push));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
