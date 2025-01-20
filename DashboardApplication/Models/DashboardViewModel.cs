using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DashboardApplication.Models
{
    public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public List<User> Users { get; set; }
        public List<Activity> RecentActivities { get; set; }
        public List<int> UserGrowthData { get; set; } // New Property to hold the monthly user data
    }
}
