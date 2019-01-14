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
    public class EventsController : Controller
    {
        private e_healthcare_dbEntities db = new e_healthcare_dbEntities();

        // GET: Events
        public ActionResult Index()
        {
            return View(db.tblEvents.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEvent tblEvent = db.tblEvents.Find(id);
            if (tblEvent == null)
            {
                return HttpNotFound();
            }
            return View(tblEvent);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intEventID,strEventTitle,strEventDetails,dtmDate")] tblEvent tblEvent)
        {
            if (ModelState.IsValid)
            {
                db.tblEvents.Add(tblEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblEvent);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEvent tblEvent = db.tblEvents.Find(id);
            if (tblEvent == null)
            {
                return HttpNotFound();
            }
            return View(tblEvent);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intEventID,strEventTitle,strEventDetails,dtmDate")] tblEvent tblEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblEvent);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEvent tblEvent = db.tblEvents.Find(id);
            if (tblEvent == null)
            {
                return HttpNotFound();
            }
            return View(tblEvent);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEvent tblEvent = db.tblEvents.Find(id);
            db.tblEvents.Remove(tblEvent);
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
