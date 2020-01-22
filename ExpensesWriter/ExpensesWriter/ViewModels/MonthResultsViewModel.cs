﻿using ExpensesWriter.Models;
using ExpensesWriter.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class MonthResultsViewModel : BaseExpenseViewModel
    {
        private ObservableCollection<CategoryExpense> categoryExpenses;
        public ObservableCollection<CategoryExpense> CategoryExpenses
        {
            get
            {
                return categoryExpenses;
            }
            set
            {
                categoryExpenses = value;
                OnPropertyChanged();
            }
        }

        private double totalMoney;
        public double TotalMoney
        {
            get
            {
                return totalMoney;
            }
            set
            {
                totalMoney = value;
                OnPropertyChanged();
            }
        }


        public Command LoadMonthResultsCommand { get; set; }

        public MonthResultsViewModel()
        {
            LoadMonthResultsCommand = new Command(async () => await ExecuteLoadMonthResultsCommand());
            LoadMonthResultsCommand.Execute(null);
        }

        private async Task ExecuteLoadMonthResultsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                CategoryExpenses = await (new MonthResultsService().GetCurrentMonthResults());
                TotalMoney = CategoryExpenses.Sum(expense => expense.Money);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }


        }
    }
}
