using MovieStore.Models;
using MovieStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var loggedUserId = CookieController.GetCookie("userId");
            if (loggedUserId != null)
            {
                var orders = from order in db.Orders
                             where order.UserId.ToString().Equals(loggedUserId)
                             select order;
                return View(orders);
            }
            return View(new List<Order>());
        }
        public ActionResult OrderDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return PartialView(order);
        }
    }
}