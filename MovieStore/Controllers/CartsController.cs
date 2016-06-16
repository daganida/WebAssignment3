using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieStore.Models;
using System.Web.UI.WebControls;
using System.Web.UI;
using MovieStore.Utilities;
using System.Globalization;

namespace MovieStore.Controllers
{
    [UserRole]
    public class CartsController : Controller
    {
        private MovieStoreEntities db = new MovieStoreEntities();

        // GET: Carts
        public ActionResult Index()
        {

            if (TempData["AmountMessage"] != null)
            {

                ViewBag.errorAmount = TempData["AmountMessage"].ToString();
            }
            
            string loggedId = CookieController.GetCookie("userId");
            var carts = db.Carts.SqlQuery("Select * from dbo.Carts where dbo.Carts.UserId = {0}",loggedId);
            if (loggedId != null)
            {
                setValueToCurrentUser(loggedId);
                Console.WriteLine("user is logged - " + loggedId);
                return View(carts.ToList());

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            /*
            ViewBag.itemsTotalValue = 100;
            ViewBag.itemsTotalDifferentProducts = 5;
            ViewBag.cartOwner = "idan";
             * */
  

        }

        private void setValueToCurrentUser(string loggedId)
        {
            int totalDifferentProducts = 0;
            double totalAmountValue = 0;
            string userName = "";
            var userCart = db.Carts.SqlQuery("Select * from dbo.Carts where dbo.Carts.UserId = {0} ",loggedId);

            foreach (Cart c in userCart)
            {
                totalDifferentProducts++;
                var currMoviePrice = db.Movies.SqlQuery("Select * from dbo.Movies where dbo.Movies.MovieId = {0}", c.MovieId).ToList();
                totalAmountValue += c.Count * currMoviePrice[0].Price;      
            }
            ViewBag.itemsTotalValue = totalAmountValue;
            ViewBag.itemsTotalDifferentProducts = totalDifferentProducts;
             userName = db.Users.SqlQuery("Select * from dbo.Users where dbo.Users.UserId = {0}",loggedId).ToList()[0].UserName;
             ViewBag.cartOwner = userName;
            

        }

        // GET: Carts/Details/5

        // GET: Carts/Create
        public ActionResult Create()
        {
            if (TempData["AmountMessage"] != null)
            {

                ViewBag.errorAmount = TempData["AmountMessage"].ToString();
            }
            ViewBag.MovieId = new SelectList(db.Movies, "MovieId", "Title");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }


        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            int quantity = cart.Count;
            quantity++;
            // in case there are no more items in the storage
            if (quantity > db.Movies.Find(cart.MovieId).Amount)
            {
                string Error = String.Format("The amount in the storage for {0}  is not avaiable",Data.getInstance().getDictionary()[cart.MovieId]);
                TempData["AmountMessage"] = Error;
                return RedirectToAction("Index");
            }
            else
            {

                db.Database.ExecuteSqlCommand("UPDATE dbo.Carts SET Count = Count+1 WHERE CartId = {0}", id);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "CartId,UserId,MovieId,Count")]*/ Cart cart)
        {
     
            if (cart.Count > db.Movies.Find(cart.MovieId).Amount)
            {
                string Error = String.Format("The amount in the storage for {0}  is not avaiable", Data.getInstance().getDictionary()[cart.MovieId]);
                TempData["AmountMessage"] = Error;
                return RedirectToAction("Create");
            }
            //string userLoggedIn =CookieController.GetCookie("userId");
            string userLoggedIn = CookieController.GetCookie("userId");
            if (userLoggedIn != null)
            {
                cart.UserId = Convert.ToInt32(userLoggedIn);
                cart.Movie = db.Movies.Find(cart.MovieId);
                cart.User = db.Users.Find(cart.UserId);
                if (ModelState.IsValid)
                {
                  
                    //first we check if item already in the cart, if yes we just want 
                    //to add to quantity
                    bool isMovieInCart = db.Carts.SqlQuery("Select * from dbo.Carts where dbo.Carts.MovieId = {0} and dbo.Carts.UserId = {1}", cart.MovieId, userLoggedIn).ToList().Count > 0;
               

                    //meaning the movie for the user is already in the cart, we want to increase amount.
                    if (isMovieInCart)
                    {
                        var carts = db.Carts.SqlQuery("Select * from dbo.Carts where dbo.Carts.MovieId = {0} and dbo.Carts.UserId = {1}", cart.MovieId, userLoggedIn);
                        cart.CartId = carts.ToList()[0].CartId;
                        string command = String.Format("UPDATE dbo.Carts SET Count = Count + {0} WHERE CartId = {1}", cart.Count.ToString(),cart.CartId);
                        db.Database.ExecuteSqlCommand(command);
                        db.SaveChanges();
                    }
                    else
                    {

                        db.Carts.Add(cart);
                        db.SaveChanges();

                    }
                  
                    return RedirectToAction("Index");
                }
            }

            ViewBag.MovieId = new SelectList(db.Movies, "MovieId", "Title", cart.MovieId);
                
           // ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", cart.UserId);
            return View(cart);
        }

        // GET: Carts/Edit/5
      
    
        // GET: Carts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cart cart = db.Carts.Find(id);
            if (cart == null)
            {
                return HttpNotFound();
            }
            int quantity = cart.Count;
            //only one item, meaning we want to delete the record from the databse
            if (quantity == 1)
            {
                db.Carts.Remove(cart);
                db.SaveChanges();
                setValueToCurrentUser(cart.UserId.ToString());

            }
            else if (quantity >= 1)
           
            {
                db.Database.ExecuteSqlCommand("UPDATE dbo.Carts SET Count = Count-1 WHERE CartId = {0}",id);
               db.SaveChanges();
             //  setValueToCurrentUser(cart.UserId.ToString());



            }
            return RedirectToAction("Index");
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
 

        protected override void Dispose(bool disposing)
        {
            
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult PaymentHandler()
        {
            ViewBag.Date = DateTime.Now.ToString();
            ViewBag.itemsTotalValue = 0;
            ViewBag.itemsTotalDifferentProducts = 0;
            ViewBag.cartOwner = "";
            double itemsValue = 0;
            int differentProducts = 0;
            User userAccount = null;
            string userLoggedIn = CookieController.GetCookie("userId");
            //first we need the user that is logged in
            if (ModelState.IsValid)
            {
                var carts = db.Carts.SqlQuery("Select * from dbo.Carts where dbo.Carts.UserId = {0}", userLoggedIn);


                //take all users cart by user Id
                foreach (Cart c in carts)
                {
                    differentProducts++;
                    var currMoviePrice = db.Movies.SqlQuery("Select * from dbo.Movies where dbo.Movies.MovieId = {0}", c.MovieId).ToList();
                    itemsValue += c.Count * currMoviePrice[0].Price;
                }
                ViewBag.itemsTotalValue = itemsValue;
                ViewBag.itemsTotalDifferentProducts = differentProducts;
                int UserId = Convert.ToInt32(userLoggedIn);
                userAccount = db.Users.Find(UserId);
                ViewBag.cartOwner = db.Users.Find(UserId).UserName;

                //first last mail cell address
                ViewBag.first = userAccount.FirstName;
                ViewBag.last = userAccount.LastName;
                ViewBag.mail = userAccount.Email;
                if (userAccount.Cellular == null)
                {
                    ViewBag.cell = "Wasn't Provide";
                }
                else
                {
                    ViewBag.cell = userAccount.Cellular;
                }
                if (userAccount.Address == null)
                {
                    ViewBag.address = "Wasn't Provide";

                }
                else
                {

                    ViewBag.address = userAccount.Address;
                }
                
            }
            return View();
        }

        public ActionResult ConfirmTransaction(string   Date)
        {
            string userLoggedIn = CookieController.GetCookie("userId");
            double totalOrderValue = 0;
            Order o = new Order()
            {
                Date = DateTime.Now,
                UserId = Convert.ToInt32(userLoggedIn),
            };
           db.Orders.Add(o);
            db.SaveChanges();
            
            if (ModelState.IsValid)
            {
                //now to reduce all items from database
                IList<Cart> carts =(IList<Cart>) db.Carts.SqlQuery("Select * from dbo.Carts where dbo.Carts.UserId = {0}", userLoggedIn).ToList();

                foreach (Cart c in carts)
                {
                    
                    //we need to update MovieOrders,Orders,Cart,Movies

                    //first lets remove from Movies.
                    db.Database.ExecuteSqlCommand("UPDATE dbo.Movies SET Amount = Amount- {0} WHERE MovieId = {1}",c.Count,c.MovieId);
                    var currMoviePrice = db.Movies.SqlQuery("Select * from dbo.Movies where dbo.Movies.MovieId = {0}", c.MovieId).ToList();
                    totalOrderValue += c.Count * currMoviePrice[0].Price;

                    //now we create new movieOrder for each cart
                    MovieOrder mo = new MovieOrder()
                    {
                        OrderId = o.OrderId,Amount = c.Count,MovieId = c.MovieId
                    };
                    db.MovieOrders.Add(mo);
                    //now we remove the cart.
                    db.Carts.Remove(c);
                    //now we add to orders
                    //now we add remove from Cart         
                }
                
                db.Database.ExecuteSqlCommand("UPDATE dbo.Orders SET TotalAmountValue = TotalAmountValue + {0} WHERE OrderId = {1}", totalOrderValue, o.OrderId);
                db.SaveChanges();
                ViewBag.orderId = o.OrderId;
            }

            return RedirectToAction("Index", "Home");          

        }
       
    }
}
