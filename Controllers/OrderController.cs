using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Library.Controllers
{
    [CustomAuthorize(Roles = "Admin,Worker")]
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Order
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Cart).Include(o => o.User);
            return View(orders.ToList());
        }
        [CustomAuthorize(Roles = "User,Admin,Worker")]
        public ActionResult UserOrders()
        {
            var user = User.Identity.GetUserId();
            var orders = db.Orders.Include(o => o.Cart).Include(o => o.User).Where(o => o.userId == user);
            return View(orders.ToList());
        }

        // GET: Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            var items = db.CartItems.Where(c => c.CartID == order.CartID).ToList();
            List<string> list = new List<string>();
            foreach (var item in items)
            {
                list.Add(item.Book.Title);
            }
            ViewBag.books = list;
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.CartID = new SelectList(db.Cart, "CartID", "userId");
            ViewBag.userId = new SelectList(db.ApplicationUsers, "Id", "Name");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,status,userId,CartID")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CartID = new SelectList(db.Cart, "CartID", "userId", order.CartID);
            ViewBag.userId = new SelectList(db.ApplicationUsers, "Id", "Name", order.userId);
            return View(order);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CartID = new SelectList(db.Cart, "CartID", "userId", order.CartID);
            //ViewBag.userId = new SelectList(db.ApplicationUsers, "Id", "Name", order.userId);
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,status,userId,CartID")] Order order)
        {
            if (ModelState.IsValid)
            {
                List<CartItem> books = db.CartItems.Where(c => c.CartID == order.CartID).ToList();
                if (order.status == Status.received)
                {
                    foreach(var item in books)
                    {
                        var book = db.Books.Where(b => b.ISBN == item.ISBN).FirstOrDefault();
                        book.Amount--;
                        db.SaveChanges();
                    }
                }
                else if (order.status == Status.returned)
                {
                    foreach (var item in books)
                    {
                        var book = db.Books.Where(b => b.ISBN == item.ISBN).FirstOrDefault();
                        book.Amount++;
                        db.SaveChanges();
                    }
                }
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CartID = new SelectList(db.Cart, "CartID", "userId", order.CartID);
            ViewBag.userId = new SelectList(db.ApplicationUsers, "Id", "Name", order.userId);
            return View(order);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
