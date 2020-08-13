using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpensesWriter.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace ExpensesWriter.Repositories.Local
{
    public class FamilyMembersDataStore
    {
        readonly SQLiteAsyncConnection connection;

        public FamilyMembersDataStore()
        {
            string path = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "ExpensesSQLite.db3");
            connection = new SQLiteAsyncConnection(path);
            connection.CreateTableAsync<FamilyMember>().Wait();
        }

        public async Task<IEnumerable<FamilyMember>> GetItemsAsync(bool forceRefresh = false)
        {
            var items = await connection.GetAllWithChildrenAsync<FamilyMember>();
            return items;
        }

        public async Task AddItemsAsync(IEnumerable<FamilyMember> members)
        {
            foreach (var member in members)
            {
                if (await GetItemAsync(member.UserId) == null)
                {
                    await connection.InsertWithChildrenAsync(member);
                }
                else
                {
                    await connection.UpdateWithChildrenAsync(member);
                }
            }
        }

        public async Task<FamilyMember> GetItemAsync(string id)
        {
            return await connection.Table<FamilyMember>()
                .FirstOrDefaultAsync(s => s.UserId == id);
        }



    }
}
