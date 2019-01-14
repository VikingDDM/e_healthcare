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
    public class DieseaseController : Controller
    {
        private e_healthcare_dbEntities db = new e_healthcare_dbEntities();

        // GET: Diesease
        public ActionResult Index()
        {
            return View(db.tblDieseases.ToList());
        }

        // GET: Diesease/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDiesease tblDiesease = db.tblDieseases.Find(id);
            if (tblDiesease == null)
            {
                return HttpNotFound();
            }
            return View(tblDiesease);
        }

        // GET: Diesease/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Diesease/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intDiseaseID,strDiseasedName,strDiseasedDetails")] tblDiesease tblDiesease)
        {
            if (ModelState.IsValid)
            {
                db.tblDieseases.Add(tblDiesease);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblDiesease);
        }

        // GET: Diesease/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDiesease tblDiesease = db.tblDieseases.Find(id);
            if (tblDiesease == null)
            {
                return HttpNotFound();
            }
            return View(tblDiesease);
        }

        // POST: Diesease/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intDiseaseID,strDiseasedName,strDiseasedDetails")] tblDiesease tblDiesease)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDiesease).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblDiesease);
        }

        // GET: Diesease/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDiesease tblDiesease = db.tblDieseases.Find(id);
            if (tblDiesease == null)
            {
                return HttpNotFound();
            }
            return View(tblDiesease);
        }

        // POST: Diesease/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDiesease tblDiesease = db.tblDieseases.Find(id);
            db.tblDieseases.Remove(tblDiesease);
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
