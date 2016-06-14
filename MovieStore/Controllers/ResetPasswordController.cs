using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStore.ViewModel;
namespace MovieStore.Controllers

{
            [UserRole]

    public class ResetPasswordController : Controller
    {
        MovieStoreEntities db = new MovieStoreEntities();
        string currUserName;
        // GET: ResetPassword
        [ActionName("Index")]
        public ActionResult Index()
        {
            if(TempData["Question"] != null) {
                ViewBag.Question = TempData["Question"];
            }
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

                }
                else
                {
                    User u = db.Users.Find(userList.ToList()[0].UserId);
                    if (u != null)
                    {
                        if (true)
                        {
                            currUserName = u.UserName;
                            var User = db.Users.SqlQuery("Select * from dbo.Users where dbo.Users.UserName = {0}", currUserName).ToList()[0];
                            int QuestionId = (int)((User)User).QuestionId;
                            string Question = db.Questions.SqlQuery("Select * from dbo.Questions where QuestionId = {0}", QuestionId).ToList()[0].Title;
                            TempData["Question"] = Question;
                            return RedirectToAction("Index");
                            //meaning he was right about the question and the answer
                        }
                        else
                        {
                            message = String.Format("One or more cradentials is incorrect.");
                            ViewBag.errorMessage = message;

                        }

                    }

                }
                ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Title");
                return View();
            
        }

      


    }
}