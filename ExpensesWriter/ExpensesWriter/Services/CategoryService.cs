﻿using ExpensesWriter.Models;
using ExpensesWriter.Repositories.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesWriter.Services
{
    public class CategoryService
    {
        public async Task<IEnumerable<BudgetItem>> GetCategoriesAsync()
        {
            //get from local storage if there is some
            var localStorage = new Repositories.Local.CategoriesDataStore();
            var items = await localStorage.GetItemsAsync();

            //update local storage if empty
            ///TODO: add update logic
            if(items.Count() == 0)
            {
                var externalStorage = new CategoriesAzureDataStore();
                items = await externalStorage.GetItemsAsync(true);

                if(items.Count() > 0)
                {
                    await localStorage.AddItemsAsync(items);
                }
            }
            return items;

        }
    }
}
