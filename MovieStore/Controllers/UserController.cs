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

namespace MovieStore.Controllers
{

    public class UserController : Controller
    {
        MovieStoreEntities db = new MovieStoreEntities();

        // GET: User
        public ActionResult Index()
        {
            if(db.Countries.ToList().Count == 0)
            LoadXML();
            ViewBag.CountryId = new SelectList(db.Countries, "ID", "Name");
            ViewBag.QuestionId = new SelectList(db.Questions, "QuestionId", "Title");
            

            
            return View();

          

        }
        public  ListItemCollection LoadXML()
        {
            DropDownList DropDownList1 = new DropDownList();
            string myXMLfile = Server.MapPath("~/countries.xml");
            DataSet dsStudent = new DataSet();

                dsStudent.ReadXml(myXMLfile);
                DropDownList1.DataSource = dsStudent;
                DropDownList1.DataValueField = "ID";
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataBind();
                int counter = 1;

                foreach (ListItem item in DropDownList1.Items)
                {
                    Country c = new Country()
                    {
                        ID = counter,Name = item.Text
             
                    };
                    db.Countries.Add(c);
                    counter++;     

                }
                db.SaveChanges();


            return DropDownList1.Items;

        }
        [HttpPost]

        public ActionResult Index( User u)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User();
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

                return RedirectToAction("Index", "Login");

            }

            return View();

        }
      



     
        
   

       

    }  
}