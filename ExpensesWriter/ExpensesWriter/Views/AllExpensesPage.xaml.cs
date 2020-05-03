﻿using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

using ExpensesWriter.Models;
using ExpensesWriter.ViewModels;
using ExpensesWriter.Helpers;

namespace ExpensesWriter.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AllExpensesPage : ContentPage
    {
        public AllExpensesPage()
        {
            InitializeComponent();

            BindingContext = new AllExpensesViewModel();
        }

        async void AddExpense_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewExpensePage()));
        }


    }
}