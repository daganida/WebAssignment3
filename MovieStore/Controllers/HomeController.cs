using MovieStore.Models;
using MovieStore.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            List<Movie> movieList = getMostFivePopularMovies(); 
   
            return View(movieList);
        }

        private List<Movie> getMostFivePopularMovies()
        {
            Data data = Data.getInstance();
            data.updateDictionary();
            List<Movie> movieList = new List<Movie>();
            Dictionary<string, int> moviesRented = new Dictionary<string, int>();
            var movies = db.Movies;
            int count = db.MovieOrders.ToList().Count;
            DateTime date = DateTime.Today.AddDays(-7);
            var movieOrderList = db.MovieOrders.SqlQuery(@"SELECT * FROM dbo.MovieOrders INNER JOIN dbo.Orders on dbo.MovieOrders.OrderId = dbo.Orders.OrderId and Date >= {0}",date);


            //for each movie we cound all orders
            foreach (MovieOrder mo in movieOrderList)
            {
                if (!moviesRented.ContainsKey(data.getDictionary()[mo.MovieId]))
                {
                    moviesRented.Add(data.getDictionary()[mo.MovieId], mo.Amount);

                }
                else moviesRented[data.getDictionary()[mo.MovieId]] += mo.Amount;           
            }

            List<KeyValuePair<string,int>> myList = moviesRented.ToList();

            myList.Sort(
                delegate(KeyValuePair<string, int> pair1,
                KeyValuePair<string, int> pair2)
                {
                    return pair2.Value.CompareTo(pair1.Value);
                }
            );


            List<Movie> solutionList = new List<Movie>();
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



    } 
}
