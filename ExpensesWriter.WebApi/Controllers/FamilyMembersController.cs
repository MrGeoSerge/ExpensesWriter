using ExpensesWriter.WebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ExpensesWriter;
using Microsoft.AspNet.Identity.Owin;

namespace ExpensesWriter.WebApi.Controllers
{
    [Authorize]
    public class FamilyMembersController : ApiController
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }



        // GET: api/FamilyMembers
        public IEnumerable<FamilyMember> GetFamilyMembers()
        {
            string userId = User.Identity.GetUserId();

            ////TODO: Create FamilyMembers storage and get data from it

            //Temporaly solution
            //get my UserId
            ApplicationUser user1 = UserManager.FindById(Constants.MyUserID);
            FamilyMember member1 = new FamilyMember
            {
                UserId = user1.Id,
                UserName = user1.UserName,
            };

            ApplicationUser user2 = UserManager.FindById(Constants.MyWifesUserID);
            FamilyMember member2 = new FamilyMember
            {
                UserId = user2.Id,
                UserName = user2.UserName,
            };

            List<FamilyMember> familyMembers = new List<FamilyMember>();
            familyMembers.Add(member1);
            familyMembers.Add(member2);

            return familyMembers;
        }

    }
}
