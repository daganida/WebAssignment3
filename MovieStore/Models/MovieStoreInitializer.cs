using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class MovieStoreInitializer : DropCreateDatabaseIfModelChanges<MovieStoreEntities>
    {
        protected override void Seed(MovieStoreEntities context)
        {
            
            User u = new User()
            {
                IsAdmin = false,
                UserName = "Idan",
                UserPassword = "123456"
            };

            context.Users.Add(u);



            
        }
    }
}