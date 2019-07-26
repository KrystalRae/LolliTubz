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
            return View(new LocationManager().GetStockFillLocation(id, CurrentUser));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StockFill(StockFillModel model)
        {
            OrderManager manager = new OrderManager();
            int orderId = manager.CreateOrder(model);
            
            return RedirectToAction("OrderSummary", "Location", new { id = orderId });
        }

        public ActionResult OrderSummary(int id)
        {
            OrderManager manager = new OrderManager();            
            return View(manager.CreateOrderSummaryModel(id, CurrentUser));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderSummary(OrderSummaryModel model)
        {
            OrderManager manager = new OrderManager();
            manager.ApproveOrder(model.OrderId);
            return RedirectToAction("Index", "Locations");
        }

        public ActionResult EditOrder(int id)
        {
            OrderManager manager = new OrderManager();
            return View(manager.CreateEditOrderSummaryModel(id, CurrentUser));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder(OrderSummaryModel model)
        {
            OrderManager manager = new OrderManager();
            manager.SaveEdittedOrder(model);
            return RedirectToAction("OrderSummary", "Location", new { id = model.OrderId });
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