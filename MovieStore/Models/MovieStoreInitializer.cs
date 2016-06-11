using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class MovieStoreInitializer : DropCreateDatabaseIfModelChanges<MovieStoreEntities>
    {
        protected override void Seed(MovieStoreEntities context)
        {
            List<User> userList = new List<User>()
            {
                new User(){UserId = 1, UserName = "Beygel", Password="123456",ConfirmPassword = "123456",Email="idandagan@walla.com",FirstName="Hen",LastName="Beygel" },
                new User(){UserId = 2, UserName = "eli", Password="123456",ConfirmPassword = "123456",Email="Beygel25@walla.com",FirstName="Hen",LastName="Beygel" },
                new User(){ UserId = 3,UserName = "omri",Password="123456",ConfirmPassword = "123456",Email="Omri12@walla.com",FirstName="Omri",LastName="Dagan" },
                new User(){ UserId = 4,UserName = "Shlomi",Password="123456",ConfirmPassword = "123456",Email="Shlomi@walla.com",FirstName="Shlomi",LastName="Biton" },

                new User(){UserId = 5, UserName = "Idan", Password="123456",ConfirmPassword = "123456",Email="Idan13@walla.com" ,FirstName="Idan",LastName="Menashe" },
                new User(){UserId = 6, UserName = "Noa", Password="123456",ConfirmPassword = "123456",Email="noy1978@walla.com" ,FirstName="Noy",LastName="Bracha" },
                new User(){UserId = 7, UserName = "Yael", Password="123456",ConfirmPassword = "123456",Email="Sylvester1@walla.com",FirstName="Sylvester",LastName="Dagan"  },
                new User(){UserId = 8, UserName = "Irit", Password="123456",ConfirmPassword = "123456",Email="shahr90@walla.com",FirstName="Shahar",LastName="Hershkovitz"  },
                new User(){UserId = 9, UserName = "Amir", Password="123456",ConfirmPassword = "123456",Email="maor89@walla.com" ,FirstName="Maor",LastName="Shvartz"  },
                new User(){UserId = 10, UserName = "Yaara",Password="123456",ConfirmPassword = "123456",Email="yael983@walla.com",FirstName="Yael",LastName="Dagan"  },
                new User(){UserId = 11, UserName = "Tzivka", Password="123456",ConfirmPassword = "123456",Email="medical24@walla.com" ,FirstName="Dana",LastName="Benham"  },
                 new User(){UserId = 12, UserName = "barG", Password="123456",ConfirmPassword = "123456",Email="medical25@walla.com" ,FirstName="Dana",LastName="Benham"  },
                  new User(){UserId = 13, UserName = "royM", Password="123456",ConfirmPassword = "123456",Email="medical26@walla.com" ,FirstName="Dana",LastName="Benham"  }
            };
            foreach (User u in userList)
            {
                context.Users.Add(u);
            }

            List<Movie> movies = new List<Movie>()
            {
                new Movie(){MovieId=1, Title = "Titanic", Length =120, Price = 40,Amount=7 },
                new Movie(){MovieId=2, Title = "The Interview", Length =130, Price = 50,Amount=8  },
                new Movie(){MovieId=3,Title = "Paranormal Activity", Length =110, Price = 60,Amount=9   },
                new Movie(){MovieId=4, Title = "Entourage", Length =80, Price = 70,Amount=10  },
                new Movie(){MovieId = 5,Title = "Troy", Length =85, Price = 80,Amount=11  },
                new Movie(){MovieId=6, Title = "Gladiator", Length =90, Price = 90,Amount=12  },
                new Movie(){MovieId = 7,Title = "Neighboors", Length =95, Price = 100,Amount=13  },
                new Movie(){MovieId = 8, Title = "X-Men", Length =60, Price = 80,Amount=12  },
                new Movie(){MovieId = 9,Title = "Batman - The Dark Knight", Length =78, Price = 85,Amount=11  },
                new Movie(){MovieId = 10, Title = "Batman Returns", Length =85, Price = 70,Amount=10  },
                new Movie(){MovieId = 11, Title = "Simposons", Length =88, Price = 75,Amount=9  },
                new Movie(){MovieId = 12, Title = "About Love And Other Drugs", Length =92, Price = 60,Amount=8   }
            };
            foreach (Movie m in movies)
            {
                context.Movies.Add(m);
            }

            List<Genre> genres = new List<Genre>()
            {
                new Genre(){ GenreId=1,TItle = "Horror" },
                new Genre(){ GenreId=2,TItle = "Action"  },
                new Genre(){ GenreId=3,TItle = "Comedy" },
                new Genre(){ GenreId=4,TItle = "History"  },
                new Genre(){ GenreId=5,TItle = "Thirller"   },
                new Genre(){ GenreId=6,TItle = "Romance" },
                new Genre(){ GenreId=7,TItle = "Music"  },
                new Genre(){ GenreId=8,TItle = "History"  },
                new Genre(){ GenreId=9,TItle = "Documentary" },
            };
            foreach (Genre g in genres)
            {
                context.Genres.Add(g);
            }
            List<MovieGenre> movieGenres = new List<MovieGenre>()
            {
                new MovieGenre(){ GenreId=6,MovieId=1},
                new MovieGenre(){ GenreId=1,MovieId=3 },
                new MovieGenre(){ GenreId=2,MovieId=5 },
                new MovieGenre(){ GenreId=2,MovieId=6   },
                new MovieGenre(){ GenreId=2,MovieId=7  },
                new MovieGenre(){ GenreId=2,MovieId=8 },
                new MovieGenre(){ GenreId=2,MovieId=9 },
                new MovieGenre(){ GenreId=2,MovieId=10 },
                new MovieGenre(){ GenreId=3,MovieId = 11 },
                new MovieGenre(){ GenreId=6,MovieId = 12 }
            };
            foreach (MovieGenre mg in movieGenres)
            {
                context.MovieGenres.Add(mg);
            }

            List<Country> Countries = new List<Country>()

        {

            new Country() { ID = 1, Name= "United States" },

            new Country() { ID = 2, Name= "Canada" },

            new Country() { ID = 3, Name= "UK" },

            new Country() { ID = 4, Name= "China" },

            new Country() { ID = 5, Name= "Japan" }

        };
            foreach (Country  c in Countries)
            {
                context.Countries.Add(c);
            }

            List<Order> orderList = new List<Order>()
            {
                new Order {OrderId = 1,UserId = 1,Date = DateTime.Now,TotalAmountValue = 40 },
                new Order {OrderId = 2,UserId = 2,Date = DateTime.Now,TotalAmountValue = 130 },
                new Order {OrderId = 3, UserId = 3,Date = DateTime.Now,TotalAmountValue = 110},
                new Order {OrderId = 4,UserId = 4,Date = DateTime.Now,TotalAmountValue = 80 },
                new Order {OrderId = 5,UserId = 5,Date = DateTime.Now,TotalAmountValue = 80 },
               new Order {OrderId = 6,UserId = 6,Date = DateTime.Now,TotalAmountValue = 90 },
         
            };
            foreach (Order o in orderList)
            {
                context.Orders.Add(o);
            }


            List<MovieOrder> movieOrderList = new List<MovieOrder>()
        {
            new MovieOrder {OrderId = 1,MovieId = 1, Amount = 1,},
            new MovieOrder {OrderId = 2,MovieId = 2, Amount = 1},
            new MovieOrder {OrderId = 3,MovieId = 3, Amount = 1},
            new MovieOrder {OrderId = 4,MovieId = 4, Amount = 1},
            new MovieOrder {OrderId = 5,MovieId = 5, Amount = 1},
            new MovieOrder {OrderId = 6,MovieId = 6, Amount = 1},

        };
            foreach (MovieOrder mo in movieOrderList)
            {
                context.MovieOrders.Add(mo);
            }



            try
            {

                List<Cart> cartList = new List<Cart>()
            {
                new Cart{UserId = 1,MovieId = 1,Count = 1,CartId = 1},
                new Cart{UserId = 1,MovieId = 2, Count = 2, CartId = 2},
                new Cart{UserId = 2,MovieId = 3, Count = 3 , CartId = 3},
                new Cart{UserId = 2,MovieId = 4, Count = 4, CartId = 4},
                new Cart{UserId = 3,MovieId = 5, Count = 4 , CartId = 5},
            };
                foreach (Cart c in cartList)
                {
                    context.Carts.Add(c);
                }

            }
            catch (Exception e)
            {
                Console.Write(e);











            }
        }
    }
}