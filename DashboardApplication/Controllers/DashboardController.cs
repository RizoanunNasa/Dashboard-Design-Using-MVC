using DashboardApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DashboardApplication.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardContext _context = new DashboardContext();

        // GET: Dashboard - Display all users and other dashboard information
        public ActionResult Index()
        {
            // Retrieve all users from the database
            var users = _context.Users.ToList();

            // Prepare monthly user data for the chart
            var userGrowthData = new List<int>();
            var currentMonth = DateTime.Now.Month;
            for (int i = currentMonth; i > currentMonth - 4; i--)
            {
                int month = i <= 0 ? 12 + i : i;
                int userCountForMonth = _context.Users.Count(u => u.CreatedAt.Month == month && u.CreatedAt.Year == DateTime.Now.Year);
                userGrowthData.Add(userCountForMonth);
            }

            // Prepare the view model
            var viewModel = new DashboardViewModel
            {
                Users = users,
                TotalUsers = users.Count,
                ActiveUsers = users.Count(u => u.CreatedAt > DateTime.Now.AddDays(-30)),
                RecentActivities = _context.Activities.OrderByDescending(a => a.CreatedAt).Take(5).ToList(),
                UserGrowthData = userGrowthData
            };

            return View(viewModel);
        }

        // GET: Dashboard/CreateUser - Display user creation form
        public ActionResult CreateUser()
        {
            return View();
        }

        // POST: Dashboard/CreateUser - Add a new user to the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                user.CreatedAt = DateTime.Now;  // Set the CreatedAt timestamp
                _context.Users.Add(user);       // Add user to the database
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Dashboard/EditUser/{id} - Display user edit form
        public ActionResult EditUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();  // If user not found, return 404 error
            }
            return View(user);
        }

        // POST: Dashboard/EditUser/{id} - Update user information in the database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = _context.Users.SingleOrDefault(u => u.Id == user.Id);
                if (existingUser == null)
                {
                    return HttpNotFound();  // If user not found, return 404 error
                }

                // Update existing user with new values
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.CreatedAt = user.CreatedAt;  // Ensure the timestamp remains unchanged if not edited

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Dashboard/DeleteUser/{id} - Confirm deletion of user
        public ActionResult DeleteUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();  // If user not found, return 404 error
            }
            return View(user);
        }

        // POST: Dashboard/DeleteUser/{id} - Delete user from the database
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();  // If user not found, return 404 error
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Dashboard/DetailsUser/{id} - Display user details
        public ActionResult DetailsUser(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return HttpNotFound();  // If user not found, return 404 error
            }
            return View(user);
        }

        // GET: Dashboard/LogActivity - Display activity logging form
        public ActionResult LogActivity()
        {
            var users = _context.Users.ToList();  // Get list of users for the dropdown
            ViewBag.Users = new SelectList(users, "Id", "Name");
            return View();
        }

        // POST: Dashboard/LogActivity - Log an activity for a user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogActivity(Activity activity)
        {
            if (ModelState.IsValid)
            {
                activity.CreatedAt = DateTime.Now;  // Set the CreatedAt timestamp for activity
                _context.Activities.Add(activity);  // Add the activity to the database
                _context.SaveChanges();
                return RedirectToAction("ViewActivities");  // Redirect to the page where activities are viewed
            }

            // If the model is invalid, provide the list of users again in the dropdown
            var users = _context.Users.ToList();
            ViewBag.Users = new SelectList(users, "Id", "Name");
            return View(activity);
        }

        // GET: Dashboard/ViewActivities - View all logged activities
        public ActionResult ViewActivities()
        {
            var activities = _context.Activities.ToList();  // Retrieve all activities from the database
            return View(activities);  // Pass activities to the view
        }

        // Dispose the context when done to release resources
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
