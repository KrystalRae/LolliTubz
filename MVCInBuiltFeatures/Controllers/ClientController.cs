using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCInBuiltFeatures.Managers;
using MVCInBuiltFeatures.Models;
using System.Linq;
using System.Web.Mvc;

namespace MVCInBuiltFeatures.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index(int id)
        {
            return View(new ClientManager().GetClient(id));
        }

        public ActionResult Add()
        {
            using (var context = new ApplicationDbContext())
            {               
                return View(new AddClientViewModel());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddClientViewModel model)
        {
            new ClientManager().AddNewClient(model);
            return RedirectToAction("Add", "Location");
        }
    }
}