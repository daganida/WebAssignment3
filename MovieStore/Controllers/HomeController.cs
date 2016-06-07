using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Controllers
{
    public class HomeController : Controller
    {
        MovieStoreEntities db = new MovieStoreEntities();
        // GET: Home
        public ActionResult Index()
        {
            var users = db.Users;
            return View("Error");
        }
        public ActionResult ShowIdan()
        {

            return View(db.Users.ToList());
        }
    }
}