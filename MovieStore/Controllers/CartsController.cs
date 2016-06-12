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

namespace MovieStore.Controllers
{
    public class CartsController : Controller
    {
        private MovieStoreEntities db = new MovieStoreEntities();

        // GET: Carts
        public ActionResult Index()
        {
            //string loggedId = CookieController.GetCookie("userId");
            string loggedId = "2";
            var carts = db.Carts.SqlQuery("Select * from dbo.Carts where dbo.Carts.UserId = {0}",loggedId);
            if (loggedId != null)
            {
                setValueToCurrentUser(loggedId);
                Console.WriteLine("user is logged - " + loggedId);
            }
            /*
            ViewBag.itemsTotalValue = 100;
            ViewBag.itemsTotalDifferentProducts = 5;
            ViewBag.cartOwner = "idan";
             * */
  

            return View(carts.ToList());
        }

        private void setValueToCurrentUser(string loggedId)
        {
            int totalDifferentProducts = 0;
            loggedId = "2";
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

                db.Database.ExecuteSqlCommand("UPDATE dbo.Carts SET Count = Count+1 WHERE CartId = {0}", id);
                db.SaveChanges();
                setValueToCurrentUser(cart.UserId.ToString());
            return RedirectToAction("Index");

        }

        // POST: Carts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CartId,UserId,MovieId,Count")] Cart cart)
        {
            //string userLoggedIn =CookieController.GetCookie("userId");
            string userLoggedIn = "2";
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
                    cart.CartId = db.Carts.SqlQuery("Select * from dbo.Carts where dbo.Carts.MovieId = {0} and dbo.Carts.UserId = {1}", cart.MovieId, userLoggedIn).ToList()[0].CartId;

                    //meaning the movie for the user is already in the cart, we want to increase amount.
                    if (isMovieInCart)
                    {
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
    }
}
