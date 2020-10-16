using ExpensesWriter.Models;
using ExpensesWriter.Repositories.Local;
using ExpensesWriter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesWriter.UpdateServices
{
    public class FamilyMembersService
    {
        readonly FamilyMembersDataStore localStorage = new FamilyMembersDataStore();
        readonly FamilyMembersDataService externalStorage = new FamilyMembersDataService();



        public async Task<IEnumerable<FamilyMember>> GetFamilyMembersAsync()
        {
            //get from local storage if there are some
            var items = await localStorage.GetItemsAsync();

            if (items.Count() == 0)
            {
                items = await RenewLocalStorage();
            }

            return items;
        }

        private async Task<IEnumerable<FamilyMember>> RenewLocalStorage()
        {
            var items = await externalStorage.GetFamilyMembersAsync();

            if (items.Count() > 0)
            {
                await localStorage.AddItemsAsync(items);
                items = await localStorage.GetItemsAsync();
            }
            return items;
        }


    }
}
