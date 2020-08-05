using ExpensesWriter.Models;
using ExpensesWriter.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class FamilyNextMonthPlanningViewModel : BaseViewModel
    {
        private ObservableCollection<BudgetPlanningItem> budgetPlanningItems;

        public ObservableCollection<BudgetPlanningItem> BudgetPlanningItems 
        {
            get 
            {
                return budgetPlanningItems;
            }
            set 
            {
                budgetPlanningItems = value;
                Residue = Income - TotalMoney;
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
                Residue = Income - TotalMoney; 
                OnPropertyChanged();
            }
        }

        private double income;
        public double Income
        {
            get
            {
                return income;
            }
            set
            {
                income = value;
                Residue = Income - TotalMoney;
                OnPropertyChanged();
            }
        }

        private double residue;
        public double Residue
        {
            get
            {
                return residue;
            }
            set
            {
                residue = value;
                OnPropertyChanged();
            }
        }

        private BudgetPlanningItem selectedPlanningItem;
        public BudgetPlanningItem SelectedPlanningItem 
        {
            get 
            {
                return selectedPlanningItem;
            } 
            set 
            {
                selectedPlanningItem = value;
                OnPropertyChanged();
            } 
        
        }



        public Command LoadMonthPlanningCommand { get; set; }

        public ICommand SaveCommand
        {
            get
            {
                return new Command(async () =>
               {
                   try
                   {
                       var dataStore = new BudgetPlanningItemsAzureDataStore();

                       await dataStore.UpdateItemAsync(SelectedPlanningItem);
                   }
                   catch(Exception ex)
                   {
                       Debug.WriteLine(ex);
                   }

               });
            }
        }

        public ICommand UpdateAllCommand
        {
            get
            {
                return new Command(async () =>
               {
                   try
                   {
                       var dataStore = new BudgetPlanningItemsAzureDataStore();

                       await dataStore.UpdateMonthItemsAsync(BudgetPlanningItems);
                   }
                   catch(Exception ex)
                   {
                       Debug.WriteLine(ex);
                   }

               });
            }
        }



        public FamilyNextMonthPlanningViewModel()
        {
            Title = "Next Month Planning";
            LoadMonthPlanningCommand = new Command(async () => await ExecuteLoadNextMonthPlanningCommand());
            LoadMonthPlanningCommand.Execute(null);

        }

        private async Task ExecuteLoadNextMonthPlanningCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var items = await GetNextMonthPlanningItems();
                
                BudgetPlanningItems = new ObservableCollection<BudgetPlanningItem>(items);
                TotalMoney = BudgetPlanningItems.Sum(x => x.Money);
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

        private async Task<IEnumerable<BudgetPlanningItem>> GetNextMonthPlanningItems()
        {
            return await new BudgetPlanningItemsAzureDataStore().GetNextMonthItemsAsync(true);
        }
    }
}
