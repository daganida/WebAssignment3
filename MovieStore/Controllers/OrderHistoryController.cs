using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Controllers
{
    [UserRole]
    public class OrderHistoryController : Controller
    {
        MovieStoreEntities db = new MovieStoreEntities();
        // GET: History
        public ActionResult Index()
        {
            var orders = db.Orders;

            return View(orders);
        }
    }
}