using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardApplication.Models
{
    public class Activity
    {
            public int Id { get; set; }               // Primary key
            public string Description { get; set; }   // Description of the activity
            public int UserId { get; set; }            // Foreign key to the User table
            public DateTime CreatedAt { get; set; }    // Timestamp of when the activity was created

            // Navigation property (optional)
            public virtual User User { get; set; }     // Navigation to User (optional, if you want to include user details)
        
    }
}