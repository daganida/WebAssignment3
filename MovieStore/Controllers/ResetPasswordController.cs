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

                }
                else
                {
                    User u = db.Users.Find(userList.ToList()[0].UserId);
                    if (u != null)
                    {
                        if (u.QuestionId == user.QuestionId && u.Answer == user.Answer)
                        {
                            currUserName = u.UserName;
                            return View("ResetPassword");
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
                [HttpPost]

        public ActionResult ResetPassword(NewPassword pass)
        {

            if (ModelState.IsValid && currUserName != null)
            {
                db.Database.ExecuteSqlCommand("UPDATE dbo.Users SET dbo.Users.Passsword = {0} and dbo.Users.ConfirmPassword = {1} WHERE UserName = {2}",pass.NewPass,pass.ConfirmPassword,currUserName);
                db.SaveChanges();
                currUserName = null;           
            }

            


            return View();      

        }


    }
}