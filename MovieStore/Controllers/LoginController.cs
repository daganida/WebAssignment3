using MovieStore.Models;
using MovieStore.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Controllers
{
    public  class  LoginController : Controller
    {
        MovieStoreEntities db = new MovieStoreEntities();

        // GET: Login
        public ActionResult Index()
        {
            
            
            
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login logUser)
        {
            if (ModelState.IsValid)
            {
                var queryResults = db.Users.Where(a=>a.UserName.Equals(logUser.UserName) && a.Password.Equals(logUser.UserPassword)).ToList();
                if (queryResults.Count == 1)
                {
                    var loggedUser = queryResults[0];
                    CookieController.SetCookie("userId", loggedUser.UserId.ToString());
                    TempData["Success"] = "User was logged successfully.";
                    return View("~/Views/Home/Index.cshtml");
                }
            }
            ViewData["Error"] = "One or more cradentials do not match.";
            return View("Index");
        }

       

        
    }
}