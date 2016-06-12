using MovieStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MovieStore.Utilities
{
    public class CookieController
    {
        static readonly int COOKIE_EXPIRE_DAYS = 30;
        public static void SetCookie(string key, string value)
        {
            HttpCookie cookie = new HttpCookie(key, value);

            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                var cookieOld = HttpContext.Current.Request.Cookies[key];
                cookieOld.Expires = DateTime.Now.AddDays(COOKIE_EXPIRE_DAYS);
                cookieOld.Value = cookie.Value;
                HttpContext.Current.Response.Cookies.Add(cookieOld);
            }
            else
            {
                cookie.Expires = DateTime.Now.AddDays(COOKIE_EXPIRE_DAYS);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        public static string GetCookie(string key)
        {
            string value = null;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];

            if (cookie != null)
            {
                value = cookie.Value;
            }
            return value;
        }

        public static DateTime getDateExpries(string key)
        {
            string value = null;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];

            if (cookie != null)
            {
                return cookie.Expires;
            }
            return DateTime.Now;

        }



        public static Tuple<bool,string> RemoveCookie(string key)
        {
            string value = string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie!=null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Set(cookie);
                HttpContext.Current.Session.Clear();
                return new Tuple<bool, string>(true, "cookie deleted successfully");
            }
            return new Tuple<bool, string>(false, "no cookie to delete");
        }
    }
}