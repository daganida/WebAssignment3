using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Controllers
{
    public class LoginController : Controller
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
            var userList = db.Users.ToList();

            if (ModelState.IsValid)
            {
                var chosenUser = db.Users.Where(a=>a.UserName.Equals(logUser.UserName) && a.Password.Equals(logUser.UserPassword)).ToList();
                if (chosenUser.Count != 0)
                {
                    TempData["Success"] = "User was logged successfully.";
                    return View("~/Views/Home/Index.cshtml");
                }               
            }
            ViewData["Error"] = "One or more cradentials do not match.";
            return View("Index");
            
         

        }

       

        
    }
}