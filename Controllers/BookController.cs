using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.DAL;
using Library.Models;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Book
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Author).Include(b => b.PublishingHouse);
            return View(books.ToList());
        }

        public ActionResult SearchBook(String searchString)
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
        
        public ActionResult SearchBookByCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var category in db.Categories)
            {
                list.Add(new SelectListItem() { Value = category.Name, Text = category.Name });
            }
            foreach (var subcategory in db.Subcategories)
            {
                list.Add(new SelectListItem() { Value = subcategory.Name, Text = subcategory.Name });
            }
            ViewBag.categories = list;
            ViewBag.bookList = db.Books.ToList();
          
            return View();
        }
        [HttpPost]
        public ActionResult SearchBookByCategory(SearchBookByCategoryViewModel model)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var category in db.Categories)
            {
                list.Add(new SelectListItem() { Value = category.Name, Text = category.Name });
            }
            foreach (var subcategory in db.Subcategories)
            {
                list.Add(new SelectListItem() { Value = subcategory.Name, Text = subcategory.Name });
            }
            ViewBag.categories = list;
            if (!String.IsNullOrEmpty(model.selectedCategory))
            {
                var books = db.Books.ToList();
                books = books.Where(b => b.Subcategory.Name == model.selectedCategory || b.Subcategory.Category.Name == model.selectedCategory).ToList();

                ViewBag.bookList = books;
            }
            return View(model);
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        //[Authorize(Roles = "Admin,Worker")]
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "Name");
            List<SelectListItem> list = new List<SelectListItem>();
            List<SelectListItem> list2 = new List<SelectListItem>();
            foreach (var publishingHouse in db.PublishingHouses)
            {
                    list.Add(new SelectListItem() { Value = publishingHouse.publishingHouseID.ToString(), Text = publishingHouse.Name });
            }
            foreach (var subcategory in db.Subcategories)
            {
                list2.Add(new SelectListItem() { Value = subcategory.SubcategoryID.ToString(), Text = subcategory.Name });
            }
            ViewBag.publishingHouseID = list;
            ViewBag.SubcategoryID = list2;
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin,Worker")]
        public ActionResult Create([Bind(Include = "ISBN,Title,PublicYear,Amount,AuthorID,SubcategoryID,publishingHouseID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "Name", book.AuthorID);
            ViewBag.SubcategoryID = new SelectList(db.Subcategories, "SubcategoryID", "Name", book.SubcategoryID);
            ViewBag.publishingHouseID = new SelectList(db.PublishingHouses, "publishingHouseID", "Name", book.publishingHouseID);
            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "Name", book.AuthorID);
            ViewBag.publishingHouseID = new SelectList(db.PublishingHouses, "publishingHouseID", "Name", book.publishingHouseID);
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ISBN,Title,PublicYear,Amount,AuthorID,publishingHouseID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "Name", book.AuthorID);
            ViewBag.publishingHouseID = new SelectList(db.PublishingHouses, "publishingHouseID", "Name", book.publishingHouseID);
            return View(book);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
