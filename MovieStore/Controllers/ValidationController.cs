using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.UI;
using MovieStore.Models;
using System.Collections;

namespace MovieStore.Controllers
{
    [OutputCache(Location = OutputCacheLocation.None, NoStore = true)]
    [UserRole]
    public class ValidationController : Controller
    {
        string currUserName;


        MovieStoreEntities db = new MovieStoreEntities();

        [HttpPost]
        public JsonResult IsUID_Available(string UserName)
        {

            if (!UserExists(UserName))
            {
                currUserName = UserName;

                return Json(true, JsonRequestBehavior.AllowGet);

            }

            string suggestedUID = String.Format(CultureInfo.InvariantCulture,
                "{0} is not available.", UserName);

            for (int i = 1; i < 100; i++)
            {
                string altCandidate = UserName + i.ToString();
                if (!UserExists(altCandidate))
                {
                    suggestedUID = String.Format(CultureInfo.InvariantCulture,
                   "{0} is not available. Try {1}.", UserName, altCandidate);
                    break;
                }
            }
            return Json(suggestedUID, JsonRequestBehavior.AllowGet);
        }

      

        private bool UserExists(string Username)
        {
            var users = db.Users;
            foreach (User u in users)
            {
                if (u.UserName.ToLower().Equals(Username.ToLower()))
                {
                    return true;

                }
            }
            return false;

        }




        public JsonResult IsMail_Available(string Email)
        {
            if (Email != null)
            {

                if (!MailExist(Email))
                {

                    return Json(true, JsonRequestBehavior.AllowGet);

                }

                string suggestedUID = String.Format(CultureInfo.InvariantCulture,
                    "{0} is not available.", Email);
                return Json(suggestedUID, JsonRequestBehavior.AllowGet);
            }
            return Json("Email was not initalized",JsonRequestBehavior.AllowGet);
        }

        private bool MailExist(string mail)
        {
            var users = db.Users;
            foreach (User u in users)
            {
                if (u.Email.ToLower().Equals(mail.ToLower()))
                {
                    return true;

                }
            }
            return false;
        }
     




    }
}



            
     

    
