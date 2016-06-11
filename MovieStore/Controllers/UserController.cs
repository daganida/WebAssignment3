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
            return View();

          

        }





       
        public  ListItemCollection LoadXML()
        {
            DropDownList DropDownList1 = new DropDownList();
            string myXMLfile = Server.MapPath("~/countries.xml");
            DataSet dsStudent = new DataSet();
            try
            {
                dsStudent.ReadXml(myXMLfile);
                DropDownList1.DataSource = dsStudent;
                DropDownList1.DataValueField = "ID";
                DropDownList1.DataTextField = "Name";
                DropDownList1.DataBind();
            }
            catch (Exception ex)
            {

            }

            return DropDownList1.Items;

        }
      



     
        
   

       

    }  
}