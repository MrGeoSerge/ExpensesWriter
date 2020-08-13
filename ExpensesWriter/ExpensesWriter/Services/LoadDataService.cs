using ExpensesWriter.Models;
using ExpensesWriter.UpdateServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesWriter.Services
{
    public class LoadDataService
    {
        public async Task<bool> LoadData()
        {
            LoadCategories();
            await LoadFamilyMembers();
            await LoadExpenses();

            return true;
        }

        private async Task<IEnumerable<FamilyMember>> LoadFamilyMembers()
        {
            var familyMembersService = new FamilyMembersService();
            var result = await familyMembersService.GetFamilyMembersAsync();
            return result;
        }

        private void LoadCategories()
        {

        }

        private async Task LoadExpenses()
        {
            var expensesService = new ExpenseService();
            var expenses = await expensesService.GetExpensesAsync();

        }
    }
}
