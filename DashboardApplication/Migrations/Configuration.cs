using System;
using System.Data.Entity.Migrations;
using DashboardApplication.Models;

namespace DashboardApplication.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DashboardContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(DashboardContext context)
        {
            // Updated to use 'Name' instead of 'FirstName' and 'LastName'
            context.Users.AddOrUpdate(
                u => u.Id, // Ensure uniqueness based on the 'Id'
                new User { Name = "John Doe", Email = "john.doe@example.com", CreatedAt = DateTime.Now },
                new User { Name = "Jane Smith", Email = "jane.smith@example.com", CreatedAt = DateTime.Now }
            );
        }
    }
}
