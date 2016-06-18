using MovieStore.Models;
using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Utilities;

namespace MovieStore.Controllers
{
    [UserRole]

    public class SearchController : Controller
    {
        MovieStoreEntities db = new MovieStoreEntities();
      

        public ActionResult Index(string sortOrder, string currentFilter, string movieGenre, string searchString, int? page)
        {
            List<Movie> recommendedMovies = getRecommendedMovies();
            //<movie name,amount of matches frmo all users>

            var GenreQry = from genre in db.Genres
                           select genre.TItle;
            var GenreLst = GenreQry.ToList<string>();

            ViewBag.movieGenre = new SelectList(GenreLst);

            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = "Name";
            ViewBag.DateSortParm = "Date";
            ViewBag.PriceSortParm ="Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var movies = from s in db.MovieGenres
                           where s.Genre.TItle == movieGenre
                           select s.Movie;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title.Contains(searchString));            
            }

            switch (sortOrder)
            {
                case "Title":
                    movies = movies.OrderByDescending(s => s.Title.ToLower());
                    break;
                case "Date":
                    movies = movies.OrderByDescending(s => s.Date);
                    break;
                case "Price":
                    movies = movies.OrderByDescending(s => s.Price);
                    break;
                default:
                    movies = movies.OrderBy(s => s.Title.ToLower());
                    break;
            }

        int pageSize = 5;
            int pageNumber = (page ?? 1);
            IPagedList<Movie> list = movies.ToPagedList(pageNumber, pageSize);
            return View(new Tuple<IPagedList<Movie>,IEnumerable<Movie>>(list,recommendedMovies));
        }

        private List<Movie> getRecommendedMovies()
        {
            List<Movie> solutionList = new List<Movie>();
            //Dictionary<userId,amount> of match movies//
            Dictionary<int,int> ratedMoviesForUser = new Dictionary<int,int>();
            Dictionary<string, int> finalPrefferedMovie = new Dictionary<string, int>();

            string userLoggedIn = CookieController.GetCookie("userId");
            if (userLoggedIn != null)
            {
                var userOrders = db.Orders.SqlQuery("Select * from dbo.Orders where dbo.Orders.UserId = {0}",Convert.ToInt32(userLoggedIn));
                foreach (Order o in userOrders)
                {
                    //foreach movie order we want to find the clients the bought the movie
                    //if theres a match we add to dictionary
                    var MovieOrdersForUser = db.MovieOrders.SqlQuery("Select * from dbo.MovieOrders where dbo.MovieOrders.OrderId = {0}",o.OrderId);

                    //now we add to dictionary if there is a match
                    foreach (MovieOrder mo in MovieOrdersForUser)
                    {
                        //now we get all users for each movieId
                        var userOrdersForSpecificMovie = db.MovieOrders.SqlQuery("select * from dbo.MovieOrders where dbo.MovieOrders.MovieId = {0}",mo.MovieId);

                        foreach (MovieOrder usersMatchForMovie in userOrdersForSpecificMovie)
                        {
                            var order = db.Orders.SqlQuery("select * from dbo.Orders where dbo.Orders.OrderId = {0}",usersMatchForMovie.OrderId).ToList()[0];
                            int userId = order.UserId;
                            if (order.UserId != o.UserId)
                            {
                                if (!ratedMoviesForUser.ContainsKey(userId))
                                {
                                    ratedMoviesForUser.Add(userId, 1);
                                }
                                else
                                {
                                    ratedMoviesForUser[userId]++;
                                }
                            }


                        }

                    }
                    

                }               
                //now we want to sort the dictionary by matches
                List<KeyValuePair<int, int>> myList = ratedMoviesForUser.ToList();

                myList.Sort(
                    delegate(KeyValuePair<int, int> pair1,
                    KeyValuePair<int, int> pair2)
                    {
                        return pair2.Value.CompareTo(pair1.Value);
                    }
                );
                //now we have sort list, need to return the top 5.
                foreach (KeyValuePair<int, int> user in myList)
                {
                    //we get all order of the person
                    //and foreach order we go on all the movies
                    var allUserOrders = db.Orders.SqlQuery("select * from dbo.orders where UserId = {0}",user.Key);

                    foreach (Order order in allUserOrders)
                    {
                        //get all the movie orders for a specific order
                        var movieOrdersForUser = db.MovieOrders.SqlQuery("select * from dbo.MovieOrders where dbo.MovieOrders.OrderId = {0}",order.OrderId);
                        foreach (MovieOrder mo in movieOrdersForUser)
                        {
                            if (Data.getInstance().getDictionary().ContainsKey(mo.MovieId))
                            {
                                string movieName = Data.getInstance().getDictionary()[mo.MovieId];
                                if (finalPrefferedMovie.ContainsKey(movieName))
                                {
                                    finalPrefferedMovie[movieName]++;
                                }
                                else
                                {
                                    finalPrefferedMovie.Add(movieName, 1);
                                }
                            }

                        }

                    }

                }
                //now we have all the movies and all the matches from all users
                //we need to sort by value
                List<KeyValuePair<string, int>> myFinalList = finalPrefferedMovie.ToList();
                //the list that we suppose to return
                solutionList = new List<Movie>();

                myFinalList.Sort(
                    delegate(KeyValuePair<string, int> pair1,
                    KeyValuePair<string, int> pair2)
                    {
                        return pair2.Value.CompareTo(pair1.Value);
                    }
                );
                //we need the best 5
                int counter = 1;
                foreach (KeyValuePair<string,int> tuple in myFinalList)
                {
                    if (counter == 5)
                        break;
                    Movie m = db.Movies.SqlQuery("select * from dbo.Movies where dbo.Movies.Title = {0}",tuple.Key).ToList()[0];
                    solutionList.Add(m);
                    counter++;

                }
                solutionList = FillListWithPopularMovies(solutionList);
                
      
            }
            else
            {
                solutionList = FillListWithPopularMovies(solutionList);
            }
            return solutionList;
            
        }

        private List<Movie> FillListWithPopularMovies(List<Movie> solutionList)
        {
            Dictionary<string, int> MovieBought = new Dictionary<string, int>();
            var movieOrders = db.MovieOrders;
            int counter = solutionList.Count;
            if (counter < 5)
            {
                foreach (MovieOrder mo in movieOrders)
                {

                    int movieId = mo.MovieId;
                    if (Data.getInstance().getDictionary().ContainsKey(movieId))
                    {
                        string movieName = Data.getInstance().getDictionary()[movieId];
                        if (!MovieBought.ContainsKey(movieName))
                        {
                            MovieBought.Add(movieName, 1);
                        }
                        else
                        {
                            MovieBought[movieName]++;
                        }
                    }
                }
                //now osrt the dictionary
                List<KeyValuePair<string, int>> myFinalList = MovieBought.ToList();
                myFinalList.Sort(
                       delegate(KeyValuePair<string, int> pair1,
                       KeyValuePair<string, int> pair2)
                       {
                           return pair2.Value.CompareTo(pair1.Value);
                       }
                   );
                foreach (KeyValuePair<string, int> tuple in myFinalList)
                {
                    if (counter == 5)
                    {
                        break;
                    }
                    else
                    {
                        Movie m = db.Movies.SqlQuery("select * from dbo.Movies where dbo.Movies.Title = {0}", tuple.Key).ToList()[0];

                        if (!solutionList.Contains(m))
                        {
                            solutionList.Add(m);
                            counter++;
                        }


                    }

                }
                return solutionList;
            }
            else
            {
                return solutionList;
            }
            



          


            
        }
        public ActionResult openMovieDetails()
        {
            return PartialView("~/path/view.cshtml");
        }
    }
}