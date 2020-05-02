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
        public IQueryable<Expense> GetExpenses()
        {
            string userId = User.Identity.GetUserId();
            return db.Expenses.Where(user => user.UserId == userId);

            //return db.Expenses;
        }

        // GET: api/CurMonthExpenses
        [Route("api/CurMonthExpenses")]
        public IQueryable<Expense> GetCurMonthExpenses()
        {
            string userId = User.Identity.GetUserId();
            return db.Expenses.Where((user => user.UserId == userId && user.CreationDateTime.Month == DateTime.Today.Month));

        }

        // GET: api/PreviousMonthExpenses
        [Route("api/PreviousMonthExpenses")]
        public IQueryable<Expense> GetPreviousMonthExpenses()
        {
            string userId = User.Identity.GetUserId();
            return db.Expenses.Where((user => user.UserId == userId && user.CreationDateTime.Month == DateTime.Today.Month - 1));

        }

        // GET: api/FamilyCurrentMonthExpenses
        [Route("api/FamilyCurrentMonthExpenses")]
        public IQueryable<Expense> GetFamilyCurrentMonthExpenses()
        {
            //string userId = User.Identity.GetUserId();
            return db.Expenses.Where(user => user.CreationDateTime.Month == DateTime.Today.Month);
        }

        // GET: api/FamilyPreviousMonthExpenses
        [Route("api/FamilyPreviousMonthExpenses")]
        public IQueryable<Expense> GetFamilyPreviousMonthExpenses()
        {
            //string userId = User.Identity.GetUserId();
            return db.Expenses.Where(user => user.CreationDateTime.Month == DateTime.Today.Month - 1);
        }

        [Route("api/ExpensesForCurrentUser")]
        public IQueryable<Expense> GetExpensesForCurrentUser()
        {
            string userId = User.Identity.GetUserId();

            return db.Expenses.Where(Expense => Expense.UserId == userId);
        }

        // GET: api/Expenses/5
        [ResponseType(typeof(Expense))]
        public IHttpActionResult GetExpense(int id)
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
        [HttpPut, Route("api/expenses/put")]
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
            db.SaveChanges();


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

            NotificationsSender notificationsSender = new NotificationsSender();

            var user_Id = HttpContext.Current.User.Identity.GetUserId();
            //var result = await notificationsSender.SendNotification("fcm", $"Expense {expense.Name} on {expense.Money.ToString()} was added", user_Id);

            NotificationService notificationService = new NotificationService();

            var devices = new DevicesContext().Devices.ToList();

            var neededAndroidDevice = devices.Where((x, y) => x.UserId == user_Id && x.Platform == "Android").FirstOrDefault();
            notificationService.SendAndroidNotification(neededAndroidDevice.DeviceToken, $"Expense {expense.Name} on {expense.Money.ToString()} was added");

            var neededAppleDevice = devices.Where((x, y) => x.UserId == userId && x.Platform == "iOS").FirstOrDefault();
            notificationService.SendAppleNotification(neededAppleDevice.DeviceToken, $"Expense {expense.Name} on {expense.Money.ToString()} was added");

            return CreatedAtRoute("DefaultApi", new { id = expense.Id }, expense);
        }

        // DELETE: api/Expenses/5
        [ResponseType(typeof(Expense))]
        public IHttpActionResult DeleteExpense(int id)
        {
            Expense expense = db.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            db.Expenses.Remove(expense);
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