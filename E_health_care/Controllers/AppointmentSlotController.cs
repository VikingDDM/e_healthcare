using E_health_care.Models;
using E_health_care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_health_care.Controllers
{
    public class AppointmentSlotController : Controller
    {
        E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();

      
        public ActionResult Index()
        {
            if(Session["doctorid"]==null)
            {
                Response.Redirect("../Login/Authenticate");
            }

            int did = Convert.ToInt32(Session["doctorid"]);

            var dblist = db.tblAppointmentSlots.Where(x=>x.intDoctorID== did).ToList();
            List<AppointmentSlotModel> list = new List<AppointmentSlotModel>();

            foreach (tblAppointmentSlot S in dblist)
            {
                AppointmentSlotModel SA = new AppointmentSlotModel();
                SA.SlotID = S.intSlotID;
                SA.DoctorID =(int) S.intDoctorID;
                SA.ClinicID =(int) S.intClinicID;
                SA.Date = (DateTime)S.dtmDate;
                SA.StartTime =(DateTime) S.dtmStartTime;
                SA.EndTime = (DateTime)S.dtmEndTime;
                SA.Status = S.strStatus;

                list.Add(SA);
            }
            return View(list);
           
        }
        public ActionResult Create()
        {
            var dbalist = db.tblClinics.ToList();
            List<ClinicModel> alist = new List<ClinicModel>();

            List<SelectListItem> cliniclist = new List<SelectListItem>();
            cliniclist.Add(new SelectListItem() { Text = "Select Clinic", Value = "-1" });
            foreach (tblClinic C in dbalist)
            {
                cliniclist.Add(new SelectListItem() { Text = C.strClinicName, Value = C.intClinicID + "" });
            }

            ViewData["cliniclist"] = cliniclist;



            List<SelectListItem> Status = new List<SelectListItem>();
            Status.Add(new SelectListItem() { Text = "Available", Value = "Available" });
            Status.Add(new SelectListItem() { Text = "No Available", Value = "No Available" });
            ViewData["Status"] = Status;

            return View();
        }

        [HttpPost]
        public ActionResult Create(AppointmentSlotModel AS,FormCollection frm)
        {

            String gap = frm["Gap"];

            tblAppointmentSlot a = new tblAppointmentSlot();
            a.intSlotID = AS.SlotID;
            a.intDoctorID = Convert.ToInt32(Session["doctorid"]);
            a.intClinicID = AS.ClinicID;
            a.dtmDate = AS.Date;
            a.dtmStartTime = AS.StartTime;

            a.dtmEndTime = AS.EndTime;

            a.dtmStartTime = a.dtmDate;
            a.dtmStartTime= a.dtmStartTime.Value.AddHours(AS.StartTime.Hour);
            a.dtmStartTime=a.dtmStartTime.Value.AddMinutes(AS.StartTime.Minute );


            a.dtmEndTime = a.dtmDate.Value.AddHours(AS.EndTime.Hour);
            a.dtmEndTime = a.dtmEndTime.Value.AddMinutes(AS.EndTime.Minute);

            a.strStatus = AS.Status;

            DateTime st = a.dtmStartTime.Value ;
            DateTime end = a.dtmEndTime.Value;

            while (st <= end)
            {

                DateTime en = st.AddMinutes(Convert.ToInt32(gap));
                a.dtmStartTime = st;
                a.dtmEndTime = en;// a.dtmEndTime.Value.AddMinutes();
                db.tblAppointmentSlots.Add(a);
                db.SaveChanges();

                st = en;
            }


            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            tblAppointmentSlot t = db.tblAppointmentSlots.SingleOrDefault(x => x.intSlotID == id);
            AppointmentSlotModel P = new AppointmentSlotModel()
            {
                SlotID = t.intSlotID,
                DoctorID = (int)t.intDoctorID,
                ClinicID = (int)t.intClinicID,
                Date = (DateTime)t.dtmDate,
                StartTime = (DateTime)t.dtmStartTime,
                EndTime = (DateTime)t.dtmEndTime,
                Status = t.strStatus,
            };
            return View(P);
           
        }
        
        public ActionResult Delete(int? id)
        {
            tblAppointmentSlot t = db.tblAppointmentSlots.SingleOrDefault(x => x.intSlotID == id);
            AppointmentSlotModel P = new AppointmentSlotModel()
            {
                SlotID = t.intSlotID,
                DoctorID = (int)t.intDoctorID,
                ClinicID = (int)t.intClinicID,
                Date = (DateTime)t.dtmDate,
                StartTime = (DateTime)t.dtmStartTime,
                EndTime = (DateTime)t.dtmEndTime,
                Status = t.strStatus,
            };
            return View(P);

        }
        [HttpPost]
        public ActionResult Delete(AppointmentSlotModel P)
        {

            var d = db.tblAppointmentSlots.SingleOrDefault(x => x.intSlotID == P.SlotID);
            db.tblAppointmentSlots.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(AppointmentSlotModel am)
        {
            tblAppointmentSlot a = db.tblAppointmentSlots.SingleOrDefault(x => x.intSlotID == am.SlotID);


            if (a != null)
            {
                a.intSlotID = am.SlotID;
                a.intDoctorID = am.DoctorID;
                a.intClinicID = am.ClinicID;
                a.dtmDate = am.Date;
                a.dtmStartTime = am.StartTime;
                a.dtmEndTime = am.EndTime;
                a.strStatus = am.Status;
            
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        public ActionResult Edit(int? id)
        {

            tblAppointmentSlot t = db.tblAppointmentSlots.SingleOrDefault(x => x.intSlotID == id);
            AppointmentSlotModel P = new AppointmentSlotModel()
            {
                SlotID = t.intSlotID,
                DoctorID = (int)t.intDoctorID,
                ClinicID = (int)t.intClinicID,
                Date = (DateTime)t.dtmDate,
                StartTime = (DateTime)t.dtmStartTime,
                EndTime = (DateTime)t.dtmEndTime,
                Status = t.strStatus,
            };
            return View(P);


        }

    }
}