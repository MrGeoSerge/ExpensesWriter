using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ExpensesWriter.Models;
using System.Windows.Input;
using ExpensesWriter.Helpers;
using System.Diagnostics;
using ExpensesWriter.Services;

namespace ExpensesWriter.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.ExpenseWriter, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.ExpenseWriter:
                        MenuPages.Add(id, new NavigationPage(new CurrentMonthExpensesPage()));
                        break;
                    case (int)MenuItemType.MonthResults:
                        MenuPages.Add(id, new NavigationPage(new CurrentMonthResultsPage()));
                        break;
                    case (int)MenuItemType.MonthPlanning:
                        MenuPages.Add(id, new NavigationPage(new MonthPlanningPage()));
                        break;
                    case (int)MenuItemType.LastMonthResults:
                        MenuPages.Add(id, new NavigationPage(new LastMonthResultsPage()));
                        break;
                    case (int)MenuItemType.LastMonthExpenses:
                        MenuPages.Add(id, new NavigationPage(new LastMonthExpensesPage()));
                        break;
                    case (int)MenuItemType.FamilyCurrentMonthExpenses:
                        MenuPages.Add(id, new NavigationPage(new FamilyCurrentMonthExpensesPage()));
                        break;
                    case (int)MenuItemType.FamilyLastMonthExpenses:
                        MenuPages.Add(id, new NavigationPage(new FamilyLastMonthExpensesPage()));
                        break;
                    case (int)MenuItemType.FamilyCurrentMonthResults:
                        MenuPages.Add(id, new NavigationPage(new FamilyCurrentMonthResultsPage()));
                        break;
                    case (int)MenuItemType.FamilyLastMonthResults:
                        MenuPages.Add(id, new NavigationPage(new FamilyLastMonthResultsPage()));
                        break;
                    case (int)MenuItemType.AllExpenses:
                        MenuPages.Add(id, new NavigationPage(new AllExpensesPage()));
                        break;
                    case (int)MenuItemType.Settings:
                        MenuPages.Add(id, new NavigationPage(new SettingsPage()));
                        break;
                    case (int)MenuItemType.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                    case (int)MenuItemType.Logout:
                        LogoutCommand.Execute(null);
                        //MenuPages.Add(id, new NavigationPage(new LoginPage()));
                        Application.Current.MainPage = new LoginPage();
                        return;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await new RegisterDeviceService().UnregisterDeviceAsync();

                    Settings.AccessToken = string.Empty;
                    Settings.AccessTokenExpirationDate = DateTime.Today;
                    Debug.WriteLine(Settings.Username);
                    Settings.Username = string.Empty;
                    Debug.WriteLine(Settings.Password);
                    Settings.Password = string.Empty;


                    // navigate to LoginPage
                });
            }
        }

    }
}
