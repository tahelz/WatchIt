using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WatchIt.Models;


namespace WatchIt.DAL
{
    public class WatchItContext : DbContext
    {
        public WatchItContext() : base("WatchItContext")
        {
         //   Database.SetInitializer<WatchItContext>(null);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<WatchIt.Models.Director> Directors { get; set; }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<WatchIt.Models.Order> Orders { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}