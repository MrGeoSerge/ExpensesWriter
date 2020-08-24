using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ExpensesWriter.Models;
using ExpensesWriter.Services;
using ExpensesWriter.UpdateServices;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class ExpenseEditViewModel : BaseViewModel
    {
        private Expense expense;
        public Expense Expense {
            get
            {
                return expense;
            }
            set 
            {
                expense = value;
                OnPropertyChanged();
            } 
        }

        public ObservableCollection<string> CategoriesList => App.CategoriesList;

        private string selectedCategory;
        public string SelectedCategory 
        {
            get 
            {
                return selectedCategory;
            }
            set 
            {
                selectedCategory = value;
                OnPropertyChanged();
            } 
        }

        public ExpenseEditViewModel()
        {
            SelectedCategory = Expense.BudgetItem != null ? Expense.BudgetItem.Name : CategoriesList.FirstOrDefault();
        }
        public ExpenseEditViewModel(Expense expense = null)
        {
            Title = expense?.Name;
            Expense = expense;
            SelectedCategory = Expense.BudgetItem != null ? Expense.BudgetItem.Name : CategoriesList.FirstOrDefault();


        }
        public ICommand SaveCommand
        {
            get
            {
                return new Command( async () =>
                {
                    try
                    {
                        var budgetItem = App.BudgetItems.Where(x => x.Name == SelectedCategory).FirstOrDefault();
                        Expense.BudgetItemId = budgetItem.Id;
                        Expense.BudgetItem = budgetItem;

                        await new ExpenseService().UpdateExpense(expense);
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
            });
            }
        }

    }
}
