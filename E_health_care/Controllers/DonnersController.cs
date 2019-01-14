using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_health_care;

namespace E_health_care.Controllers
{
    public class DonnersController : Controller
    {
        private e_healthcare_dbEntities db = new e_healthcare_dbEntities();

        // GET: Donners
        public ActionResult Index()
        {
            return View(db.tblDonners.ToList());
        }

        // GET: Donners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDonner tblDonner = db.tblDonners.Find(id);
            if (tblDonner == null)
            {
                return HttpNotFound();
            }
            return View(tblDonner);
        }

        // GET: Donners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Donners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intDonnerID,strDonnerName,decAmount,strNotes,dtmDate")] tblDonner tblDonner)
        {
            if (ModelState.IsValid)
            {
                db.tblDonners.Add(tblDonner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblDonner);
        }

        // GET: Donners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDonner tblDonner = db.tblDonners.Find(id);
            if (tblDonner == null)
            {
                return HttpNotFound();
            }
            return View(tblDonner);
        }

        // POST: Donners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intDonnerID,strDonnerName,decAmount,strNotes,dtmDate")] tblDonner tblDonner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDonner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblDonner);
        }

        // GET: Donners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDonner tblDonner = db.tblDonners.Find(id);
            if (tblDonner == null)
            {
                return HttpNotFound();
            }
            return View(tblDonner);
        }

        // POST: Donners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDonner tblDonner = db.tblDonners.Find(id);
            db.tblDonners.Remove(tblDonner);
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
