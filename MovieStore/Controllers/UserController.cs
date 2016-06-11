using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace MovieStore.Controllers
{
    public class UserController : Controller
    {
        MovieStoreEntities db = new MovieStoreEntities();

        // GET: User
        public ActionResult Index()
        {
            SelectList sl = initCountry();
            return View();


        }

        private SelectList initCountry()
        {
            SelectList ListCategories = new SelectList(new[]
                            {
                                new { Value = "1", Text = "Category 1" },
                                new { Value = "2", Text = "Category 2" },
                                new { Value = "3", Text = "Category 3" },
                            }, "Value", "Text");

            return ListCategories;
            

        }   
        
   

        [HttpPost]
        public ActionResult Register(FormCollection formVars)
        {
            if (ModelState.IsValid)
            {

            }
            return View();
            
           
        }

    }  
}