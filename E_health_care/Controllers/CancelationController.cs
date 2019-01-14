using E_health_care.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_health_care.Controllers
{
    public class CancelationController : Controller
    {

         E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();

        public ActionResult DoctorCancel()
        {
            if(Session["doctorid"] == null)
            {
                Response.Redirect("../Login/Authenticate");
            }

            int did = Convert.ToInt32(Session["doctorid"].ToString());
            DateTime dt = DateTime.Now;
            var dblist = db.tblAppointmentSlots.Where (x=>(x.intDoctorID == did && x.dtmStartTime >dt)).ToList();

            var aplist = new List<AppointmentModel>();

            
            
            foreach (tblAppointmentSlot S in dblist)
            {
                AppointmentSlotModel SA = new AppointmentSlotModel();

                var app = db.tblAppointments.Where(x => x.intSlotID == S.intSlotID);
                foreach(tblAppointment P in app)
                {

                    AppointmentModel M = new AppointmentModel() { AppointmentID = P.intAppointmentID, AppotimentTakenTime = (DateTime)P.dtmAppotimentTakenTime, IsNewCase = (bool)P.blnIsNewCase, MemberID = (int) P.intMemberID, Notes = P.strNotes, OldCaseNo = P.strOldCaseNo, PatientAge = (int)P.intPatientAge, PatientGender = P.strPatientGender, PatientName = P.strPatientName, SlotID = (int)P.intSlotID };
                    aplist.Add(M);
                }
                
            }
            return View(aplist);

        }
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

        public ActionResult Create1(int id)
        {

            var slot = db.tblAppointmentSlots.SingleOrDefault(x => x.intSlotID == id);
            if(slot != null)
            {
                slot.strStatus = "Cancelled";
            } 
            
            return RedirectToAction("DoctorCancel", "Cancelation");
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

            CancelationModel M = new CancelationModel() { AppointmentID= (int)C.intAppointmentID,CancelationID = C.intCancelationID , CancelByLoginID =(int) C.intCancelByLoginID , CancelDateTime= (DateTime)C.dtmCancelDateTime , Reason= C.strReason }; 
                return View(M);
            
        }
    }
}
