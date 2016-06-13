using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStore.ViewModel;
namespace MovieStore.Controllers
{
    public class ResetPasswordController : Controller
    {
        MovieStoreEntities db = new MovieStoreEntities();
        // GET: ResetPassword
        public ActionResult Index()
        {
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Title");

            return View();
        }
        [HttpPost]

        public ActionResult Index(PasswordDelete user)
        {
            string message = "";

                var userList = db.Users.SqlQuery("Select * from dbo.Users where dbo.Users.UserName = {0}",user.UserName);
                if (userList.ToList().Count == 0)
                {
                    //meaning there's no such user
                    // we want to print a message
                     message = String.Format("User {0} was not found in the database",user.UserName);
                    ViewBag.errorMessage = message;
                    return View();
                }
                else
                {
                    User u = db.Users.Find(userList.ToList()[0].UserId);
                    if (u != null)
                    {
                        if (u.QuestionId == user.QuestionId && u.Answer == user.Answer)
                        {
                            //meaning he was right about the question and the answer
                            return View("ResetPassword");
                        }
                        else
                        {
                            message = String.Format("One or more cradentials is incorrect.");
                        }

                    }

                }
                RedirectToAction("Index");
            
        }

        public ActionResult ResetPassword(string userName)
        {


            return View();      

        }


    }
}