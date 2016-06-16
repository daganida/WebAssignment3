using MovieStore.Models;
using MovieStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieStore
{
    public class UserRoleAttribute : FilterAttribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {

            //You can do your stuff and set a viewBad value
            var ctx = new MovieStoreEntities();
            string canDoSomething = CookieController.GetCookie("userId");
            var username = "";
            if (canDoSomething != null)
            {
                username = ctx.Users.Find(Convert.ToInt32(canDoSomething)).UserName;
                string loginDate = CookieController.lastLoginDate.ToString();
                filterContext.Controller.ViewBag.lastDate = loginDate;
                filterContext.Controller.ViewBag.UserName = username;
                filterContext.Controller.ViewBag.IsAdmin = ctx.Users.Find(Convert.ToInt32(canDoSomething)).IsAdmin;
            }
       
        }
    }
}