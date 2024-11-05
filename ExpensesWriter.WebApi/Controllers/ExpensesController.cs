using ExpensesWriter.WebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.Mobile.Server.Config;
using Microsoft.Azure.Mobile.Server;
using System.Threading.Tasks;
using ExpensesWriter.WebApi.Services;

namespace ExpensesWriter.WebApi.Controllers
{
    [Authorize]
    public class ExpensesController : ApiController
    {
        private ExpensesContext db = new ExpensesContext();

        // GET: api/Expenses
        public IEnumerable<Expense> GetExpenses()
        {
            string userId = User.Identity.GetUserId();
            //return db.Expenses.Where(user => user.UserId == userId).ToList();
            var result = db.Expenses.ToList();

            return result;
        }

        // GET: api/CurMonthExpenses
        [Route("api/CurMonthExpenses")]
        public IEnumerable<Expense> GetCurMonthExpenses()
        {
            string userId = User.Identity.GetUserId();
            

            var expenses = db.Expenses.Where(expense => expense.UserId == userId && expense.CreationDateTime.Year == DateTime.Today.Year && expense.CreationDateTime.Month == DateTime.Today.Month);

            var list = expenses.ToList();
            return list;
        }

        // GET: api/LastMonthExpenses
        [Route("api/LastMonthExpenses")]
        public IEnumerable<Expense> GetLastMonthExpenses()
        {
            var lastMonthDate = DateTime.Today.AddMonths(-1);
            var lastMonthYear = lastMonthDate.Year;
            var lastMonthMonth = lastMonthDate.Month;

            string userId = User.Identity.GetUserId();
            return db.Expenses.Where(expense => expense.UserId == userId && expense.CreationDateTime.Year == lastMonthYear && expense.CreationDateTime.Month == lastMonthMonth).ToList();

        }


        // GET: api/LastModifiedExpense
        [Route("api/LastModifiedExpense")]
        public async Task<Expense> GetLastModifiedExpense()
        {
            string userId = User.Identity.GetUserId();
            Expense expense = await db.Expenses.Where(e => e.UserId == userId).OrderByDescending(x => x.ModificationDateTime).FirstOrDefaultAsync();
            return expense;
        }

        // GET: api/ModifiedExpenses
        [Route("api/ModifiedExpenses")]
        public async Task<IEnumerable<Expense>> GetModifiedExpenses()
        {
            if (Request.Headers.Contains("LastModified"))
            {
                string userId = User.Identity.GetUserId();
                string modifiedDate = Request.Headers.GetValues("LastModified").First();
                DateTime lastModified = new DateTime(long.Parse(modifiedDate));
                var expenses = await db.Expenses.Where(x => x.UserId == userId && x.ModificationDateTime > lastModified).ToListAsync();
                return expenses;
            }
            else
            {
                throw new Exception("Date of Last Modified expense was not provided");
            }
        }

        // GET: api/FamilyCurrentMonthExpenses
        [Route("api/FamilyCurrentMonthExpenses")]
        public IEnumerable<Expense> GetFamilyCurrentMonthExpenses()
        {
            //string userId = User.Identity.GetUserId();
            return db.Expenses.Where(expense => expense.CreationDateTime.Year == DateTime.Today.Year 
                                    && expense.CreationDateTime.Month == DateTime.Today.Month
                                    && !expense.IsDeleted).ToList();
        }

        // GET: api/FamilyLastMonthExpenses
        [Route("api/FamilyLastMonthExpenses")]
        public IEnumerable<Expense> GetFamilyLastMonthExpenses()
        {
            var lastMonthDate = DateTime.Today.AddMonths(-1);
            var lastMonthYear = lastMonthDate.Year;
            var lastMonthMonth = lastMonthDate.Month;
            //string userId = User.Identity.GetUserId();
            var result = db.Expenses.Where(expense => expense.CreationDateTime.Year == lastMonthYear && expense.CreationDateTime.Month == lastMonthMonth).ToList();
            return result;
        }

        [Route("api/ExpensesForCurrentUser")]
        public IEnumerable<Expense> GetExpensesForCurrentUser()
        {
            string userId = User.Identity.GetUserId();

            return db.Expenses.Where(expense => expense.UserId == userId).ToList();
        }

        // GET: api/Expenses/5
        [ResponseType(typeof(Expense))]
        public IHttpActionResult GetExpense(string id)
        {
            Expense Expense = db.Expenses.Find(id);
            if (Expense == null)
            {
                return NotFound();
            }

            return Ok(Expense);
        }

        // PUT: api/Expenses/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExpense(Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (expense.Id != expense.Id)
            //{
            //    return BadRequest();
            //}

            db.Entry(expense).State = EntityState.Modified;
            string userId = User.Identity.GetUserId();
            expense.UserId = userId;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(expense.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Expenses
        [ResponseType(typeof(Expense))]
        public async Task<IHttpActionResult> PostExpense(Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.Identity.GetUserId();
            expense.UserId = userId;

            db.Expenses.Add(expense);
            await db.SaveChangesAsync();


            //// Get the settings for the server project.
            //HttpConfiguration config = this.Configuration;
            ////MobileAppSettingsDictionary settings =
            ////    this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();

            //// Get the Notification Hubs credentials for the mobile app.
            //string listenConnectionString = "Endpoint=sb://sergiynotificationhub.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=HL9tpugxzELLM7bz/EH1XqE/uxuGHK+i6X2/jPNVCFY=";
            //string notificationHubName = "XamarinAndroidNotificationsHub";
            ////string notificationHubName = settings.NotificationHubName;
            ////string notificationHubConnection = settings
            ////    .Connections[MobileAppSettingsKeys.NotificationHubConnectionString].ConnectionString;

            //var notificationHubConnection = listenConnectionString;


            //// Create a new Notification Hub client.
            //NotificationHubClient hub = NotificationHubClient
            //.CreateClientFromConnectionString(notificationHubConnection, notificationHubName);

            //// Send the message so that all template registrations that contain "messageParam"
            //// receive the notifications. This includes APNS, GCM, WNS, and MPNS template registrations.
            //Dictionary<string, string> templateParams = new Dictionary<string, string>();
            //templateParams["messageParam"] = expense.Name + " was added to the list.";

            //try
            //{
            //    // Send the push notification and log the results.
            //    var result = await hub.SendTemplateNotificationAsync(templateParams);

            //    // Write the success result to the logs.
            //    config.Services.GetTraceWriter().Info(result.State.ToString());
            //}
            //catch (System.Exception ex)
            //{
            //    // Write the failure result to the logs.
            //    config.Services.GetTraceWriter()
            //        .Error(ex.Message, null, "Push.SendAsync Error");
            //}

            //NotificationsSender notificationsSender = new NotificationsSender();

            //var user_Id = HttpContext.Current.User.Identity.GetUserId();
            ////var result = await notificationsSender.SendNotification("fcm", $"Expense {expense.Name} on {expense.Money.ToString()} was added", user_Id);

            //NotificationService notificationService = new NotificationService();

            //var devices = new DevicesContext().Devices.ToList();

            //var neededAndroidDevice = devices.Where((x, y) => x.UserId == user_Id && x.Platform == "Android").FirstOrDefault();
            //notificationService.SendAndroidNotification(neededAndroidDevice.DeviceToken, $"Expense {expense.Name} on {expense.Money.ToString()} was added");

            //var neededAppleDevice = devices.Where((x, y) => x.UserId == userId && x.Platform == "iOS").FirstOrDefault();
            //notificationService.SendAppleNotification(neededAppleDevice.DeviceToken, $"Expense {expense.Name} on {expense.Money.ToString()} was added");

            return CreatedAtRoute("DefaultApi", new { id = expense.Id }, expense);
        }

        // DELETE: api/Expenses/5
        [ResponseType(typeof(Expense))]
        public IHttpActionResult DeleteExpense(string id)
        {
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }
            expense.IsDeleted = true;
            //db.Expenses.Remove(expense);
            db.SaveChanges();

            return Ok(expense);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ExpenseExists(string id)
        {
            return db.Expenses.Count(e => e.Id == id) > 0;
        }
    }
}