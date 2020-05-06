using ExpensesWriter.WebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace ExpensesWriter.WebApi.Controllers
{
    //[Authorize]
    public class CategoriesController : ApiController
    {
        private ExpensesContext db = new ExpensesContext();

        // GET: api/Categories
        [HttpGet, Route("api/categories")]
        public IQueryable<BudgetItem> GetCategories()
        {
            //string userId = User.Identity.GetUserId();
            //return db.Categories.Where(user => user.UserId == userId);

            var budgetItems = db.BudgetItems;
            return budgetItems;
        }


        // POST: api/Categories
        [ResponseType(typeof(BudgetItem))]
        public async Task<IHttpActionResult> PostCategory(BudgetItem budgetItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.Identity.GetUserId();
            budgetItem.UserId = userId;

            db.BudgetItems.Add(budgetItem);
            await db.SaveChangesAsync();

            return Ok();

        }



        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        [HttpPut, Route("api/BudgetItems/put")]
        public IHttpActionResult PutBudgetItem(BudgetItem budgetItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Entry(budgetItem).State = EntityState.Modified;
            string userId = User.Identity.GetUserId();
            budgetItem.UserId = userId;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetItemExists(budgetItem.Id))
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


        private bool BudgetItemExists(int id)
        {
            return db.BudgetItems.Count(e => e.Id == id) > 0;
        }

    }
}