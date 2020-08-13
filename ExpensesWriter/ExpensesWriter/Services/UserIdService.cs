using ExpensesWriter.Helpers;
using ExpensesWriter.Repositories.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesWriter.Services
{
    public class UserIdService
    {
        private string userId;
        public string UserId
        {
            get
            {
                if (String.IsNullOrEmpty(userId))
                {
                    if (String.IsNullOrEmpty(Settings.UserId))
                    {
                        var familyMembers = new FamilyMembersDataStore().GetItemsAsync().GetAwaiter().GetResult();
                        userId = familyMembers.Where(x => x.UserName.ToLower().Trim() == Settings.Username.ToLower().Trim()).FirstOrDefault().UserId;
                        Settings.UserId = userId;
                    }
                    else
                    {
                        userId = Settings.UserId;
                    }
                }
                return userId;
            }
            set
            {
                userId = value;
            }
        }

        public async Task<String> GetUserIdAsync()
        {
            if (String.IsNullOrEmpty(userId))
            {
                if (String.IsNullOrEmpty(Settings.UserId))
                {
                    var familyMembers = await new FamilyMembersDataStore().GetItemsAsync();
                    userId = familyMembers.Where(x => x.UserName.ToLower().Trim() == Settings.Username.ToLower().Trim()).FirstOrDefault().UserId;
                    Settings.UserId = userId;
                }
                else
                {
                    userId = Settings.UserId;
                }
            }
            return userId;
        }

    }
}

