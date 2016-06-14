using MovieStore.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using PagedList;

namespace MovieStore.Controllers
{
    [UserRole]

    public class SearchController : Controller
    {
        MovieStoreEntities db = new MovieStoreEntities();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
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

            var movies = from s in db.Movies
                           select s;

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
    }
}