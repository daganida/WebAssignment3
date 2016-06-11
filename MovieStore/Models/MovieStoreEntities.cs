using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MovieStore.Models
{
    public class MovieStoreEntities : DbContext
    {

        public DbSet<Cart> Carts { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public  DbSet<MovieOrder> MovieOrders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

       


     
    }







}