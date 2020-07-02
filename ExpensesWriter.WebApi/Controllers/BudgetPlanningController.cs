using ExpensesWriter.WebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExpensesWriter.WebApi.Controllers
{
    [Authorize]
    public class BudgetPlanningController : ApiController
    {
        private ExpensesContext db = new ExpensesContext();

        // GET: api/CurMonthExpenses
        [Route("api/CurMonthBudgetPlanningItems")]
        public async Task<IEnumerable<BudgetPlanningItem>> GetCurMonthBudgetPlanningItems()
        {
            string userId = User.Identity.GetUserId();

            var items = db.BudgetPlanningItems.Where(item => item.PlanningMonth.Year == DateTime.Now.Year && item.PlanningMonth.Month == DateTime.Now.Month).ToList();

            if(items.Count == 0)
            {
                await CreateBudgetForCurrentMonth(userId);
                items = db.BudgetPlanningItems.Where(item => item.PlanningMonth.Year == DateTime.Now.Year && item.PlanningMonth.Month == DateTime.Now.Month).ToList();
            }

            return items;
        }

        // GET: api/LastMonthExpenses
        [Route("api/LastMonthBudgetPlanningItems")]
        public async Task<IEnumerable<BudgetPlanningItem>> GetLastMonthBudgetPlanningItems()
        {
            string userId = User.Identity.GetUserId();

            var items = db.BudgetPlanningItems.Where(item => item.PlanningMonth.Year == DateTime.Now.Year && item.PlanningMonth.Month == DateTime.Now.Month - 1).ToList();

            if(items.Count == 0)
            {
                await CreateBudgetForCurrentMonth(userId);
                items = db.BudgetPlanningItems.Where(item => item.PlanningMonth.Year == DateTime.Now.Year && item.PlanningMonth.Month == DateTime.Now.Month).ToList();
            }

            return items;
        }

        // GET: api/NextMonthExpenses
        [Route("api/NextMonthBudgetPlanningItems")]
        public async Task<IEnumerable<BudgetPlanningItem>> GetNextMonthBudgetPlanningItems()
        {
            string userId = User.Identity.GetUserId();

            var items = db.BudgetPlanningItems.Where(item => item.PlanningMonth.Year == DateTime.Now.Year && item.PlanningMonth.Month == DateTime.Now.Month + 1).ToList();

            if(items.Count == 0)
            {
                await CreateBudgetForNextMonth(userId);
                items = db.BudgetPlanningItems.Where(item => item.PlanningMonth.Year == DateTime.Now.Year && item.PlanningMonth.Month == DateTime.Now.Month).ToList();
            }

            return items;
        }

        [HttpPut, Route("api/UpdatePlanningItem")]
        public async Task UpdatePlanningItemExpense(BudgetPlanningItem budgetPlanningItem)
        {
            await UpdateItemAsync(budgetPlanningItem);
        }

        [HttpPut, Route("api/UpdateMonthPlanningItems")]
        public async Task UpdateMonthPlanningItemsAsync(List<BudgetPlanningItem> budgetPlanningItems)
        {
            foreach(var item in budgetPlanningItems)
            {
                await UpdateItemAsync(item);
            }
        }

        private async Task UpdateItemAsync(BudgetPlanningItem budgetPlanningItem)
        {
            var item = db.BudgetPlanningItems.Find(budgetPlanningItem.Id);

            if(item != null)
            {
                db.BudgetPlanningItems.AddOrUpdate(budgetPlanningItem);
                await db.SaveChangesAsync();
            }
        }

        private async Task CreateBudgetForCurrentMonth(string userId)
        {
            var budgetItems = db.BudgetItems.ToList();

            var planningList = new List<BudgetPlanningItem>();
            foreach(var item in budgetItems)
            {
                BudgetPlanningItem budgetPlanningItem = new BudgetPlanningItem
                {
                    BudgetItemId = item.Id,
                    PlanningMonth = DateTime.Today,
                    Money = 0,
                    UserId = userId
                };
                planningList.Add(budgetPlanningItem);
            }
                db.BudgetPlanningItems.AddRange(planningList);
                await db.SaveChangesAsync();
        }

        private async Task CreateBudgetForNextMonth(string userId)
        {
            var budgetItems = db.BudgetItems.ToList();

            DateTime firstDayOfNextMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month + 1, 1);
            var planningList = new List<BudgetPlanningItem>();
            foreach(var item in budgetItems)
            {

                BudgetPlanningItem budgetPlanningItem = new BudgetPlanningItem
                {
                    BudgetItemId = item.Id,
                    PlanningMonth = firstDayOfNextMonth,
                    Money = 0,
                    UserId = userId
                };
                planningList.Add(budgetPlanningItem);
            }
                db.BudgetPlanningItems.AddRange(planningList);
                await db.SaveChangesAsync();
        }
    }
}
