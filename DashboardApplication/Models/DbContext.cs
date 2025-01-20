using System.Data.Entity;

namespace DashboardApplication.Models
{
    public class DashboardContext : DbContext
    {
        // Constructor to set the connection string
        public DashboardContext() : base("name=DefaultConnection")
        {
        }

        // DbSets represent tables in your database
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
    }
}
