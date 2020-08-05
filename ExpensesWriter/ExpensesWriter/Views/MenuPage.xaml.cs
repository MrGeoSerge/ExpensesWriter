using ExpensesWriter.Enums;
using ExpensesWriter.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpensesWriter.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem { Id = MenuItemType.PersonalCurrentMonthExpenses, Title = "Personal Current Month Expenses" },
                new HomeMenuItem { Id = MenuItemType.FamilyCurrentMonthResults, Title = "Family Current Month Results" },
                new HomeMenuItem { Id = MenuItemType.FamilyCurrentMonthPlanning, Title = "Family Current Month Planning" },
                new HomeMenuItem { Id = MenuItemType.FamilyCurrentMonthExpenses, Title = "Family Current Month Expenses" },
                new HomeMenuItem { Id = MenuItemType.FamilyNextMonthPlanning, Title = "Family Next Month Planning" },
                new HomeMenuItem { Id = MenuItemType.FamilyLastMonthExpenses, Title = "Family Last Month Expenses" },
                new HomeMenuItem { Id = MenuItemType.FamilyLastMonthResults, Title = "Family Last Month Resuls" },
                new HomeMenuItem { Id = MenuItemType.PersonalCurrentMonthResults, Title = "Personal Current Month Results" },
                new HomeMenuItem { Id = MenuItemType.PersonalLastMonthExpenses, Title = "Personal Last Month Expenses" },
                new HomeMenuItem { Id = MenuItemType.PersonalLastMonthResults, Title = "Personal Last Month Results"},
                new HomeMenuItem { Id = MenuItemType.FamilyAllExpenses, Title = "Family All Expenses" },
                new HomeMenuItem { Id = MenuItemType.Settings, Title = "Settings" },
                new HomeMenuItem { Id = MenuItemType.About, Title = "About" },
                new HomeMenuItem { Id = MenuItemType.Logout, Title = "Logout"}
            };

            ListViewMenu.ItemsSource = menuItems;

            ListViewMenu.SelectedItem = menuItems[0];
            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id);
            };
        }
    }
}