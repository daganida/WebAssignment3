using MovieStore.Models;
using MovieStore.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieStore.ViewModel;

namespace MovieStore.Controllers
{
    public class HomeController : Controller
    {
        MovieStoreEntities db = new MovieStoreEntities();
        // GET: Home
        public ActionResult Index()
        
        {
            string loggedId = CookieController.GetCookie("userId");
            if (loggedId!=null)
            {
               Console.WriteLine("user is logged - "+loggedId);
            }


            List<Movie> top5 = getMostFivePopularMovies("TopFivePopular");
            List<Movie> recMovies = getMostFivePopularMovies("TopRecent");
            /*
            TwoListViewModel TwoListViewModel = new TwoListViewModel
            {
                Recent = recMovies,
                TopFive = top5  
            };
             */

            return View(top5);

         

   
        }

        
        private List<Movie> getMostFivePopularMovies(string Case)
        {
            List<Movie> solutionList = new List<Movie>();
            Data data = Data.getInstance();
            data.updateDictionary();
            DateTime date;
            List<Movie> movieList = new List<Movie>();
            Dictionary<string, int> moviesRented = new Dictionary<string, int>();
            var movies = db.Movies;
            int count = db.MovieOrders.ToList().Count;


            if (Case.Equals("TopFivePopular"))
            {
                 date = DateTime.Today.AddDays(-7);

                var movieOrderList = db.MovieOrders.SqlQuery(@"SELECT * FROM dbo.MovieOrders INNER JOIN dbo.Orders on dbo.MovieOrders.OrderId = dbo.Orders.OrderId and Date >= {0}", date);
                //for each movie we cound all orders
                foreach (MovieOrder mo in movieOrderList)
                {
                    if (!moviesRented.ContainsKey(data.getDictionary()[mo.MovieId]))
                    {
                        moviesRented.Add(data.getDictionary()[mo.MovieId], mo.Amount);

                    }
                    else moviesRented[data.getDictionary()[mo.MovieId]] += mo.Amount;
                }

                List<KeyValuePair<string, int>> myList = moviesRented.ToList();

                myList.Sort(
                    delegate(KeyValuePair<string, int> pair1,
                    KeyValuePair<string, int> pair2)
                    {
                        return pair1.Key.CompareTo(pair2.Key);
                    }
                );


                 solutionList = new List<Movie>();
                int counter = 0;
                foreach (KeyValuePair<string, int> movie in myList)
                {
                    var MovieChosen = db.Movies.Where(a => a.Title.Equals(movie.Key)).ToList();
                    solutionList.Add((Movie)MovieChosen[0]);
                    counter++;
                    if (counter >= 5)
                        break;
                }
                return solutionList;
            }

            else if (Case.Equals("TopRecent"))
            {
                date = DateTime.Today.AddDays(-30);
                var list = db.Movies;
                var movieOrderList = db.Movies.SqlQuery(@"SELECT * FROM dbo.Movies WHERE dbo.Movies.Date >= {0}", date);


                List<Movie> myList = movieOrderList.ToList();

                myList.Sort(
                    delegate(Movie pair1,
                    Movie pair2)
                    {
                        return pair1.Title.CompareTo(pair2.Title);
                    }
                );
                return myList;
            }

                /*
                 solutionList = new List<Movie>();
                int counter = 0;
                foreach (KeyValuePair<string, int> movie in myList)
                {
                    var MovieChosen = db.Movies.Where(a => a.Title.Equals(movie.Key)).ToList();
                    solutionList.Add((Movie)MovieChosen[0]);
                    counter++;
                    if (counter >= 5)
                        break;
                 * */
                
            else
            {
                return new List<Movie>();
            }


            }



        
        public ActionResult redirectToLogin()
        {
            return RedirectToAction("Index","Login");
        }
        public ActionResult redirectToRegisteration()
        {
            return RedirectToAction("Index", "User");
        }

    } 
}
