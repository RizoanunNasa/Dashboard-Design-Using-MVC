using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardApplication.Models
{
    public class User
    {
        public int Id { get; set; }             // Corresponds to the 'Id' column in the table
        public string Name { get; set; }        // Corresponds to the 'Name' column in the table
        public string Email { get; set; }       // Corresponds to the 'Email' column in the table
        public DateTime CreatedAt { get; set; } // Corresponds to the 'CreatedAt' column in the table
    }
}