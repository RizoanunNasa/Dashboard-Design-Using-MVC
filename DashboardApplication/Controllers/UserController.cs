using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DashboardApplication.Models;  // Ensure this namespace is included

public class UserController : Controller
{
    private readonly DashboardContext _context;

    public UserController()
    {
        _context = new DashboardContext();
    }

    // Index action to get list of users
    public ActionResult Index()
    {
        var users = _context.Users.ToList();
        return View(users);  // Pass the list of users to the view
    }
}
