using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCInBuiltFeatures.Managers;
using MVCInBuiltFeatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCInBuiltFeatures.Controllers
{
    public class StockOrderController : Controller
    {
       
        public ActionResult Index()
        {
            var currentUser = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())).FindById(User.Identity.GetUserId());
            OrderManager manager = new OrderManager();
            return View(manager.CreateStockOrderModel(currentUser.FranchiseId));
        }
    }
}