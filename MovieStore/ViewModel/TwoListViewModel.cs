using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieStore.ViewModel
{
    public class TwoListViewModel
    {
        MovieStoreEntities db = new MovieStoreEntities();

        public List<Movie> TopFive { get; set; }
        public List<Movie> Recent { get; set; }
    }
}