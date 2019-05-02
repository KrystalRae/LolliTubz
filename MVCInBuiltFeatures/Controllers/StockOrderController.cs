using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCInBuiltFeatures.Controllers
{
    public class StockOrderController : Controller
    {
        // GET: StockOrder
        public ActionResult Index()
        {
            return View();
        }
    }
}