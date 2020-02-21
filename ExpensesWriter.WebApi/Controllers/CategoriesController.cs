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
    [Authorize]
    public class CategoriesController : ApiController
    {
        private CategoriesContext db = new CategoriesContext();

        // GET: api/Categories
        public IQueryable<Category> GetCategories()
        {
            string userId = User.Identity.GetUserId();
            return db.Categories.Where(user => user.UserId == userId);
        }


        // POST: api/Categories
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.Identity.GetUserId();
            category.UserId = userId;

            db.Categories.Add(category);
            await db.SaveChangesAsync();

            return Ok();

        }



        // PUT: api/Categories/5
        [ResponseType(typeof(void))]
        [HttpPut, Route("api/categories/put")]
        public IHttpActionResult PutCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            db.Entry(category).State = EntityState.Modified;
            string userId = User.Identity.GetUserId();
            category.UserId = userId;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
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


        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }

    }
}