using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExpensesWriter.WebApi.Models
{
    public class CategoriesContext : DbContext
    {
        public CategoriesContext() : base("name=ExpensesWriterDB")
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}