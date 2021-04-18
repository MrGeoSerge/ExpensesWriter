using ExpensesWriter.Models;
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
    public class PersonalCurrentMonthResultsViewModel : BaseViewModel
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

        private double totalPlannedMoney;
        public double TotalPlannedMoney
        {
            get
            {
                return totalPlannedMoney;
            }
            set
            {
                totalPlannedMoney = value;
                OnPropertyChanged();
            }
        }

        private double percentOfExecution;
        public double PercentOfExecution
        {
            get
            {
                return percentOfExecution;
            }
            set
            {
                percentOfExecution = value;
                OnPropertyChanged();
            }
        }



        public Command LoadMonthResultsCommand { get; set; }

        public PersonalCurrentMonthResultsViewModel()
        {
            Title = "Current Month Results";
            LoadMonthResultsCommand = new Command(async () => await ExecuteLoadMonthResultsCommand());
            LoadMonthResultsCommand.Execute(null);
        }

        protected async Task ExecuteLoadMonthResultsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                CategoryExpenses = await GetCategoryExpenses();
                TotalMoney = CategoryExpenses.Sum(expense => expense.Money);
                TotalPlannedMoney = CategoryExpenses.Sum(expense => expense.PlannedMoney);
                PercentOfExecution = CalculatePercentOfExecution();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            finally
            {
                IsBusy = false;
            }


        }

        private int CalculatePercentOfExecution()
        {
            return (int)(TotalMoney / TotalPlannedMoney * 100);
        }

        protected virtual async Task<ObservableCollection<CategoryExpense>> GetCategoryExpenses()
        {
            var result = await new MonthResultsService().GetCurrentMonthResults();

            return result;
        }
    }
}
