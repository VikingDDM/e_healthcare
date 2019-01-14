using E_health_care.Controllers;
using E_health_care.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_health_care.Controllers
{
    public class MemberCancelationController : Controller
    {


         E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();
        public ActionResult Index()
        {

            var dblist = db.tblCancelations.ToList();
            List<Models.CancelationModel> list = new List<Models.CancelationModel>();

            foreach (tblCancelation C in dblist)
            {
                Models.CancelationModel ca = new Models.CancelationModel();
                ca.CancelationID = C.intCancelationID;
                ca.AppointmentID = (int)C.intAppointmentID;
                ca.CancelByLoginID = (int)C.intCancelByLoginID;
                ca.Reason = C.strReason;
                ca.CancelDateTime = (DateTime)C.dtmCancelDateTime;

                list.Add(ca);
            }
            return View(list);

        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.CancelationModel CA)
        {
            tblCancelation c = new tblCancelation();
            c.intCancelationID = CA.CancelationID;
            c.intAppointmentID = CA.AppointmentID;
            c.intCancelByLoginID = CA.CancelByLoginID;
            c.strReason = CA.Reason;
            c.dtmCancelDateTime = CA.CancelDateTime;

            db.tblCancelations.Add(c);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            tblCancelation C = db.tblCancelations.SingleOrDefault(x => x.intCancelationID == id);
            Models.CancelationModel N = new Models.CancelationModel()
            {
                CancelationID = C.intCancelationID,
                AppointmentID = (int)C.intAppointmentID,
                CancelByLoginID = (int)C.intCancelByLoginID,
                Reason = C.strReason,
                CancelDateTime = (DateTime)C.dtmCancelDateTime,
            };
            return View(N);

        }
        public ActionResult Delete(int? id)
        {
            tblCancelation C = db.tblCancelations.SingleOrDefault(x => x.intCancelationID == id);
            Models.CancelationModel N = new Models.CancelationModel()
            {
                CancelationID = C.intCancelationID,
                AppointmentID = (int)C.intAppointmentID,
                CancelByLoginID = (int)C.intCancelByLoginID,
                Reason = C.strReason,
                CancelDateTime = (DateTime)C.dtmCancelDateTime,
            };
            return View(N);

        }
        [HttpPost]
        public ActionResult Delete(Models.CancelationModel N)
        {

            var d = db.tblCancelations.SingleOrDefault(x => x.intCancelationID == N.CancelationID);
            db.tblCancelations.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(Models.CancelationModel ca)
        {
            tblCancelation c = db.tblCancelations.SingleOrDefault(x => x.intCancelationID == ca.CancelationID);


            if (c != null)
            {

                c.intCancelationID = ca.CancelationID;
                c.intAppointmentID = ca.AppointmentID;
                c.intCancelByLoginID = ca.CancelByLoginID;
                c.strReason = ca.Reason;
                c.dtmCancelDateTime = ca.CancelDateTime;


                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            tblCancelation C = db.tblCancelations.SingleOrDefault(x => x.intCancelationID == id);

            CancelationModel M = new CancelationModel() { AppointmentID = (int)C.intAppointmentID, CancelationID = C.intCancelationID, CancelByLoginID = (int)C.intCancelByLoginID, CancelDateTime = (DateTime)C.dtmCancelDateTime, Reason = C.strReason };
            return View(M);
           

        }
    }
}






