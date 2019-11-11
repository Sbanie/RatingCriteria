using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RatingCriteria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RatingCriteria.Controllers
{
    [Authorize(Roles = "Super Admin")]
    public class AdminController : Controller
    {


        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(FormCollection form)
        {
            string rolename = form["RoleName"];
            var roleManager = new RoleManager<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists(rolename))
            {
                //create super admin role
                var role = new IdentityRole(rolename);
                roleManager.Create(role);
            }
            return RedirectToAction("CreateUserAndAssignRole", "Admin");
        }

        public ActionResult CreateUserAndAssignRole()
        {
            ViewBag.Roles = db.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult CreateUserAndAssignRole(FormCollection form)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            string UserName = form["txtEmail"];
            string email = form["txtEmail"];
            string pwd = form["txtPassword"];

            //create default user

            var user = new ApplicationUser();

            user.UserName = UserName;
            user.UserName = email;
            user.Email = UserName;

            //string password = pwd;

            var newuser = userManager.Create(user, pwd);
            string rol = form["RoleName"];
            ApplicationUser users = db.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
          //  userManager.AddToRole(user.Id, rol);
            return View("Dashboard");
        }
    }
}