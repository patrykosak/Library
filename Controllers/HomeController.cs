using Library.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(String searchString)
        {

            ViewBag.bookList = db.Books.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                var books = db.Books.ToList();
                books = books.Where(b => b.Title.Contains(searchString) || b.Author.Name.Contains(searchString)
                     || b.Author.Surname.Contains(searchString)
                     || b.PublishingHouse.Name.Contains(searchString)).ToList();
                ViewBag.bookList = books;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Change(String LanguageAbbrevation)
        {
            if (LanguageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbrevation;
            Response.Cookies.Add(cookie);
            ViewBag.bookList = db.Books.ToList();
            return View("Index");

        }
        public ActionResult ChangeTheme(int id)
        {

            switch (id)
            {
                case 1:
                    MvcApplication.theme = "bootstrap.min.css";
                    break;
                case 2:
                    MvcApplication.theme = "bootstrapcolor.min.css";
                    break;

                case 3:
                    MvcApplication.theme = "bootstrapdark.min.css";
                    break;
            }


            return RedirectToAction("Index", "Home");

        }

    }
}