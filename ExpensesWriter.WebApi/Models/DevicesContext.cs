using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ExpensesWriter.WebApi.Models
{
    public class DevicesContext : DbContext
    {
        public DevicesContext() : base("name=ExpensesWriterDB")
        {
        }

        public DbSet<Device> Devices { get; set; }

    }
}