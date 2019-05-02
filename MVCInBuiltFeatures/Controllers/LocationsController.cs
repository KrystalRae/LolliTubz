using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCInBuiltFeatures.Managers;
using MVCInBuiltFeatures.Models;
using System.Web.Mvc;

namespace MVCInBuiltFeatures.Controllers
{
    public class LocationsController : Controller
    {
        // GET: Locations
        public ActionResult Index()
        {
            var currentUser = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())).FindById(User.Identity.GetUserId());
            var viewModel = new LocationManager().GetAllFranchiseLocations(currentUser.FranchiseId);

            return View(viewModel);
        }
    }
}