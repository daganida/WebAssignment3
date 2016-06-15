using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;
using MovieStore;
using System.Data;
using System.Web.UI.WebControls;
using System.Collections;

namespace MovieStore.Controllers

{
    [UserRole]

    public class UserController : Controller
    {
        MovieStoreEntities db = new MovieStoreEntities();

        // GET: User
        public ActionResult Index()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "ID", "Name");
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Title");
            var categories = db.Genres.Select(c => new
            {
                GenreId = c.GenreId,
                Title = c.TItle
            }).ToList();
            ViewBag.GenreList = new MultiSelectList(categories, "GenreId", "Title");

            return View();

          

        }
      
        [HttpPost]

        public ActionResult Index(User u,object [] GenreList)
        {
            MultiSelectList l = ViewBag.GenreList;
            List<string> genres = new List<string>();
             User newUser = new User();

            if (ModelState.IsValid)
            {
               
                newUser.UserName = u.UserName;
                newUser.Password = u.Password;
                newUser.ConfirmPassword = u.ConfirmPassword;
                newUser.CreditCardNumber = u.CreditCardNumber;
                newUser.Email = u.Email;
                newUser.FirstName = u.FirstName;
                newUser.LastName = u.LastName;
                newUser.QuestionId = u.QuestionId;
                newUser.Answer = u.Answer;
                newUser.City = u.City;
                newUser.Address = u.Address;
                newUser.CountryId = u.CountryId;
                newUser.Cellular = u.Cellular;
                newUser.Phone = u.Phone;
                db.Users.Add(newUser);
                //save the user
                db.SaveChanges();
                //save the question for the user.
                UserQuestion uq = new UserQuestion();
                uq.UserId = newUser.UserId;
                uq.QuestionId = newUser.QuestionId;
                db.UserQuestions.Add(uq);
                db.SaveChanges();
                foreach (object o in GenreList)
                {
                    UserGenre ug = new UserGenre();
                    ug.GenreId = Convert.ToInt32(o);
                    ug.UserId = newUser.UserId;
                    db.UserGenres.Add(ug);

                }
                db.SaveChanges();
                return RedirectToAction("Index", "Login");

                

            }
            return View();
           
        }
      



     
        
   

       

    }  
}