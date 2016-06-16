using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Controllers
{
    public class StoreManagerController : Controller
    {
        MovieStoreEntities db = new MovieStoreEntities();
        // GET: StoreManager
        public ActionResult Index()
        {
            return View();
        }
    }
}