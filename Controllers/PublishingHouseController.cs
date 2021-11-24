﻿using System;
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
    public class PublishingHouseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PublishingHouse
        public ActionResult Index()
        {
            return View(db.PublishingHouses.ToList());
        }

        // GET: PublishingHouse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PublishingHouse publishingHouse = db.PublishingHouses.Find(id);
            if (publishingHouse == null)
            {
                return HttpNotFound();
            }
            return View(publishingHouse);
        }

        // GET: PublishingHouse/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PublishingHouse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "publishingHouseID,Name")] PublishingHouse publishingHouse)
        {
            if (ModelState.IsValid)
            {
                db.PublishingHouses.Add(publishingHouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publishingHouse);
        }

        // GET: PublishingHouse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PublishingHouse publishingHouse = db.PublishingHouses.Find(id);
            if (publishingHouse == null)
            {
                return HttpNotFound();
            }
            return View(publishingHouse);
        }

        // POST: PublishingHouse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "publishingHouseID,Name")] PublishingHouse publishingHouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publishingHouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publishingHouse);
        }

        // GET: PublishingHouse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PublishingHouse publishingHouse = db.PublishingHouses.Find(id);
            if (publishingHouse == null)
            {
                return HttpNotFound();
            }
            return View(publishingHouse);
        }

        // POST: PublishingHouse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PublishingHouse publishingHouse = db.PublishingHouses.Find(id);
            db.PublishingHouses.Remove(publishingHouse);
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
