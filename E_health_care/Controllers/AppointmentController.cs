using E_health_care.Models;
using E_health_care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace E_health_care.Controllers
{
    public class AppointmentController : Controller
    {
        e_healthcare_dbEntities db = new e_healthcare_dbEntities();

        public ActionResult Index()
        {

            if(Session["usertype"] ==null)
            {
                Response.Redirect("../Login/Authenticate");
            }

            var dblist = db.tblAppointments.ToList();

            if (Session["usertype"].ToString() == "member")
            {
                int mid = Convert.ToInt32(Session["memberid"]);
                dblist = db.tblAppointments.Where(x => x.intMemberID == mid).ToList();
            }
            List<AppointmentModel> list = new List<AppointmentModel>();

            foreach (tblAppointment A in dblist)
            {
                AppointmentModel ap = new AppointmentModel();
                ap.AppointmentID = A.intAppointmentID;
                ap.SlotID =(int) A.intSlotID;
                ap.MemberID =(int)A.intMemberID;
                ap.PatientName = A.strPatientName;
                ap.PatientAge =(int) A.intPatientAge;
                ap.PatientGender = A.strPatientGender;
                ap.Notes = A.strNotes;
                ap.IsNewCase =(bool) A.blnIsNewCase;
                ap.OldCaseNo = A.strOldCaseNo;
                ap.AppotimentTakenTime =(DateTime) A.dtmAppotimentTakenTime;
                list.Add(ap);
            }
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }  
        [HttpPost]
        public ActionResult Create(AppointmentModel A)
        {
            tblAppointment ap = new tblAppointment();
            ap.intSlotID = A.SlotID;
            ap.intMemberID = A.MemberID;
            ap.strPatientName = A.PatientName;
            ap.intPatientAge = A.PatientAge;
            ap.strPatientGender = A.PatientGender;
            ap.strNotes = A.Notes;
            ap.blnIsNewCase = A.IsNewCase;
            ap.strOldCaseNo = A.OldCaseNo;
            ap.dtmAppotimentTakenTime = A.AppotimentTakenTime;

            db.tblAppointments.Add(ap);
            db.SaveChanges();


            //Mail Code Here
            String em = Session["email"].ToString();
            String sub = "Your Appointment Booked";

            String det = "\nAppointment ID " + ap.intAppointmentID;
            det += "\nFor :" + A.PatientName;


            SmtpClient cl = new SmtpClient("smtp.gmail.com", 587);
            cl.Credentials = new NetworkCredential("fake74053@gmail.com", "fake123456");
            cl.EnableSsl = true;

            MailMessage mg = new MailMessage("fake74053@gmail.com", em, sub, det);
            cl.Send(mg);

            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            tblAppointment t = db.tblAppointments.SingleOrDefault(x => x.intAppointmentID == id);
            AppointmentModel A = new AppointmentModel()
            {
                AppointmentID =t.intAppointmentID,
                SlotID = (int)t.intSlotID,
                MemberID = (int)t.intMemberID,
                PatientName= t.strPatientName,
                PatientAge=(int) t.intPatientAge,
                PatientGender = t.strPatientGender,
                Notes = t.strNotes,
                IsNewCase = (bool)t.blnIsNewCase,
                OldCaseNo = t.strOldCaseNo,
                AppotimentTakenTime =(DateTime) t.dtmAppotimentTakenTime,

            };
            return View(A);
        }

        public ActionResult Delete(int ?id)
        {
            tblAppointment t = db.tblAppointments.SingleOrDefault(x => x.intAppointmentID == id);
            if (t == null)
                Response.Redirect("../Login/Authenticate");
            AppointmentModel A = new AppointmentModel()
            {

                AppointmentID = t.intAppointmentID,
                SlotID = (int)t.intSlotID,
                MemberID = (int)t.intMemberID,
                PatientName = t.strPatientName,
                PatientAge = (int)t.intPatientAge,
                PatientGender = t.strPatientGender,
                Notes = t.strNotes,
                IsNewCase = (bool)t.blnIsNewCase,
                OldCaseNo = t.strOldCaseNo,
                AppotimentTakenTime = (DateTime)t.dtmAppotimentTakenTime,


            };
            return View(A);

        }
        [HttpPost]
        public ActionResult Delete(AppointmentModel A)
        {

            var d = db.tblAppointments.SingleOrDefault(x => x.intAppointmentID == A.AppointmentID);
            db.tblAppointments.Remove(d);
            db.SaveChanges();
            if(Session ["usertype"].ToString ().ToLower()=="doctor")
            return RedirectToAction("Index","DoctorUser");
            else
                return RedirectToAction("Index", "MemberUser");

        }
        [HttpPost]
        public ActionResult Edit(AppointmentModel am)
        {
            tblAppointment a = db.tblAppointments.SingleOrDefault(x => x.intAppointmentID == am.AppointmentID);
            

            if (a != null)
            {
                a.intAppointmentID = am.AppointmentID;
                a.intSlotID = am.SlotID;
                a.intMemberID = am.MemberID;
                a.strPatientName = am.PatientName;
                a.intPatientAge = am.PatientAge;
                a.strPatientGender = am.PatientGender;
                a.strNotes = am.Notes;
                a.blnIsNewCase = am.IsNewCase;
                a.strOldCaseNo = am.OldCaseNo;
                a.dtmAppotimentTakenTime = am.AppotimentTakenTime;

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ?id)
        {
            tblAppointment t = db.tblAppointments.SingleOrDefault(x => x.intAppointmentID == id);
            AppointmentModel A = new AppointmentModel()
            {
                AppointmentID = t.intAppointmentID,
                SlotID = (int)t.intSlotID,
                MemberID = (int)t.intMemberID,
                PatientName = t.strPatientName,
                PatientAge = (int)t.intPatientAge,
                PatientGender = t.strPatientGender,
                Notes = t.strNotes,
                IsNewCase = (bool)t.blnIsNewCase,
                OldCaseNo = t.strOldCaseNo,
                AppotimentTakenTime = (DateTime)t.dtmAppotimentTakenTime,

            };
            return View(A);
        }
    }
}
