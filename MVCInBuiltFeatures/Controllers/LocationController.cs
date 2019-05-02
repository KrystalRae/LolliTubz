using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCInBuiltFeatures.Managers;
using MVCInBuiltFeatures.Models;
using System.Linq;
using System.Web.Mvc;

namespace MVCInBuiltFeatures.Controllers
{
    public class LocationController : Controller
    {
        private ApplicationUser CurrentUser
        {
            get
            {
                return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())).FindById(User.Identity.GetUserId());
            }
        }

        // GET: Location
        public ActionResult Index(int id)
        {
            return View(new LocationManager().GetLocation(id, CurrentUser));
        }

        public ActionResult StockFill(int id)
        {
            return View(new LocationManager().GetLocation(id, CurrentUser));
        }

        public ActionResult Edit(int id)
        {
            return View(new LocationManager().EditLocation(id, CurrentUser));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Location location)
        {
            new LocationManager().SaveLocation(location);
            return RedirectToAction("Index", "Locations");
        }

        public ActionResult Add()
        {
            return View(new LocationManager().AddLocation());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddLocationViewModel model)
        {
            new LocationManager().AddNewFranchiseLocation(CurrentUser.FranchiseId, model);
            return RedirectToAction("Index", "Locations");
        }
    }
}