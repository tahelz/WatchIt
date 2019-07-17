using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WatchIt.Models;


namespace WatchIt.DAL
{
    public class WatchItContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public WatchItContext() : base("DefaultConnection")
        {
            Database.SetInitializer<WatchItContext>(null);
        }
    }

}