using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MovieStore.Utilities.Searching
{
    class SearchManager
    {
        MovieStoreEntities db = new MovieStoreEntities();

        public async Task<IQueryable<Movie>> GetFilteredMoviesAsync(string movieGenre, string searchString)
        {
            return db.Movies.Where(m => m.MovieGenres.ToList().Find(mg => mg.Genre.TItle.Contains(movieGenre)) != null &&
                                                                           m.Title.Contains(searchString));
        }
    }
}