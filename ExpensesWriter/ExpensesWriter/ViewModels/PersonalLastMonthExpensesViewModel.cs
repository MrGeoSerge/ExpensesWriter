using ExpensesWriter.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpensesWriter.ViewModels
{
    public class PersonalLastMonthExpensesViewModel : ExpensesViewModel
    {

        public PersonalLastMonthExpensesViewModel()
        {
            Title = "Last Month Expenses";
        }

        protected override async Task<IEnumerable<Expense>> GetExpenses()
        {
            return await DataStore.GetLastMonthItemsAsync(true);
        }


    }
}
