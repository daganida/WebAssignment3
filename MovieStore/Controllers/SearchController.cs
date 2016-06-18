using MovieStore.Models;
using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using MovieStore.Utilities;

namespace MovieStore.Controllers
{
    [UserRole]

    public class SearchController : Controller
    {
        MovieStoreEntities db = new MovieStoreEntities();
      

        public ActionResult Index(string sortOrder, string currentFilter, string movieGenre, string searchString, int? page)
        {

            var GenreQry = from genre in db.Genres
                           select genre.TItle;
            var GenreLst = GenreQry.ToList<string>();

            ViewBag.movieGenre = new SelectList(GenreLst);

            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = "Name";
            ViewBag.DateSortParm = "Date";
            ViewBag.PriceSortParm ="Price";
            movieGenre = String.IsNullOrEmpty(movieGenre) ? "" : movieGenre;
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
                           where s.Genre.TItle.Contains(movieGenre)
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
            IPagedList list = movies.ToPagedList(pageNumber, pageSize);
            return View(list);
        }
        public ActionResult openMovieDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return PartialView("~/Views/Home/MovieDetails.cshtml", movie);
        }

        public ActionResult BuyMovie(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = CookieController.GetCookie("userId");

            CartsController.addNewCartToDB(Int32.Parse(userId), id.Value);
            return RedirectToAction("Index");
        }
    }
}