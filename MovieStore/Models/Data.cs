using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class Data
    {
        MovieStoreEntities db = new MovieStoreEntities();
        private Dictionary<int, string> MovieNames;
        private static Data instance;

        private Data()
        {
            
        }

        public static Data getInstance()
        {
            if (instance == null)
            {
                instance = new Data();
                instance.MovieNames = new Dictionary<int, string>();
            }
            return instance;
        }
        public void updateDictionary()
        {
            try
            {
                var movieList = db.Movies;
                foreach (Movie m in movieList)
                {
                    MovieNames[m.MovieId] = m.Title;
                }
            }
            catch (DataException dbEx)
            {
                foreach (var validationErrors in dbEx.Data)
                {
                   
                }
            }

        }
        public Dictionary<int, string> getDictionary()
        {
            return MovieNames;
        }

    }
}