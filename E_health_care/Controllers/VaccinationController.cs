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
    public class VaccinationController : Controller
    {
        private e_healthcare_dbEntities db = new e_healthcare_dbEntities();

        // GET: Vaccination
        public ActionResult Index()
        {
            return View(db.tblVaccines.ToList());
        }

        // GET: Vaccination/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVaccine tblVaccine = db.tblVaccines.Find(id);
            if (tblVaccine == null)
            {
                return HttpNotFound();
            }
            return View(tblVaccine);
        }

        // GET: Vaccination/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vaccination/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "intVaccine,strVaccineName,strVaccineDetails")] tblVaccine tblVaccine)
        {
            if (ModelState.IsValid)
            {
                db.tblVaccines.Add(tblVaccine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblVaccine);
        }

        // GET: Vaccination/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVaccine tblVaccine = db.tblVaccines.Find(id);
            if (tblVaccine == null)
            {
                return HttpNotFound();
            }
            return View(tblVaccine);
        }

        // POST: Vaccination/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "intVaccine,strVaccineName,strVaccineDetails")] tblVaccine tblVaccine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblVaccine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblVaccine);
        }

        // GET: Vaccination/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVaccine tblVaccine = db.tblVaccines.Find(id);
            if (tblVaccine == null)
            {
                return HttpNotFound();
            }
            return View(tblVaccine);
        }

        // POST: Vaccination/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblVaccine tblVaccine = db.tblVaccines.Find(id);
            db.tblVaccines.Remove(tblVaccine);
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
