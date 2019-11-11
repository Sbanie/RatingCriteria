using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using RatingCriteria.Models;

[assembly: OwinStartupAttribute(typeof(RatingCriteria.Startup))]
namespace RatingCriteria
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }

        public void CreateUserAndRoles()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            if (!roleManager.RoleExists("Super Admin"))
            {
                //create super admin role
                var role = new IdentityRole("Super Admin");
                roleManager.Create(role);

                role = new IdentityRole("Student");
                roleManager.Create(role);

                role = new IdentityRole("Lecture");
                roleManager.Create(role);

                //create default user
                var user = new ApplicationUser();
                user.UserName = "sbaniedi@gmail.com";
                user.Email = "sbaniedi@gmail.com";
                string pwd = "Elimtende@123";

                var newuser = userManager.Create(user, pwd);
                if (newuser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Super Admin");
                }





   


            }
        }
    }
}
