using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCInBuiltFeatures.Models;
using System.Web.Mvc;

namespace MVCInBuiltFeatures.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Administrator, Franchise")]
        public ActionResult Index()
        {            
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            
            if (userManager.IsInRole(User.Identity.GetUserId(), "Administrator"))
            {
                return View("adminHome");
            }
            else
            {
                return View();
            }
        }
    }
}