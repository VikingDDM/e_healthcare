using E_health_care.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_health_care.Controllers
{
    public class DoctorTimeController : Controller
    {
         E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();
        public ActionResult Index()
        {
            int docid = -1;
            if (Session["doctorid"]==null)
            {
                return RedirectToAction("Authenticate", "Login");
            }

            else
            {
                docid = Convert.ToInt32(Session["doctorid"]);
            }
            var dblist = db.tblDoctorTimes.Where(x=>x.intDoctorID== docid).ToList();

            List<DoctorTimeModel> list = new List<DoctorTimeModel>(); 
   
            foreach (tblDoctorTime T in dblist)
            {
                DoctorTimeModel t = new DoctorTimeModel();
                t.TimeId = T.intTimeID;
                t.DoctorId =(int) T.intClinicID;
                t.ClinicId = (int)T.intClinicID;
                t.DayNo =(int) T.intDayNo;
                t.FromTime = (DateTime) T.dtmFromTime;
                t.ToTime =(DateTime) T.dtmToTime;
                t.Charges = (decimal)T.decCharges;
                list.Add(t);
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


            return View();
        }
        [HttpPost]

        public ActionResult Create(DoctorTimeModel D)
        {
            tblDoctorTime dr = new tblDoctorTime();
     //       dr.intTimeID = D.TimeId;
            dr.intDoctorID = Convert.ToInt32(Session["doctorid"].ToString());
            dr.intClinicID = D.ClinicId;
            dr.intDayNo = D.DayNo;
            dr.dtmFromTime = D.FromTime;
            dr.dtmToTime = D.ToTime;
            dr.decCharges = D.Charges;


            db.tblDoctorTimes.Add(dr);
            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult Details(int? id)
        {
            tblDoctorTime D = db.tblDoctorTimes.SingleOrDefault(x => x.intTimeID == id);
            DoctorTimeModel N = new DoctorTimeModel()
            {
                TimeId = D.intTimeID,
                DoctorId =(int) D.intDoctorID,
                ClinicId = (int)D.intClinicID,
                DayNo = (int)D.intDayNo,
                FromTime = (DateTime)D.dtmFromTime,
                ToTime = (DateTime)D.dtmToTime,
                Charges = (decimal)D.decCharges,
            };
            return View(N);
        }
        public ActionResult Delete(int? id)
        {

            tblDoctorTime D = db.tblDoctorTimes.SingleOrDefault(x => x.intTimeID == id);
            DoctorTimeModel N = new DoctorTimeModel()
            {
                TimeId = D.intTimeID,
                DoctorId = (int)D.intDoctorID,
                ClinicId = (int)D.intClinicID,
                DayNo = (int)D.intDayNo,
                FromTime = (DateTime)D.dtmFromTime,
                ToTime = (DateTime)D.dtmToTime,
                Charges = (decimal)D.decCharges,
            };
            return View(N);
        }
        [HttpPost]
        public ActionResult Delete(DoctorTimeModel N)
        {

            var d = db.tblDoctorTimes.SingleOrDefault(x => x.intTimeID == N.TimeId);
            db.tblDoctorTimes.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(DoctorTimeModel dm)
        {
            tblDoctorTime t = db.tblDoctorTimes.SingleOrDefault(x => x.intTimeID == dm.TimeId);
            
            if(t != null)
            {
                t.dtmFromTime = dm.FromTime ;
                t.dtmToTime = dm.ToTime;
                t.decCharges = dm.Charges;
                t.intClinicID = dm.ClinicId;
                t.intDayNo = dm.DayNo;
                t.intDoctorID = dm.DoctorId;
                
                db.SaveChanges();
            } 
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            tblDoctorTime D = db.tblDoctorTimes.SingleOrDefault(x => x.intTimeID == id);
            DoctorTimeModel N = new DoctorTimeModel()
            {
                TimeId = D.intTimeID,
                DoctorId = (int)D.intDoctorID,
                ClinicId = (int)D.intClinicID,
                DayNo = (int)D.intDayNo,
                FromTime = (DateTime)D.dtmFromTime,
                ToTime = (DateTime)D.dtmToTime,
                Charges = (decimal)D.decCharges,
            };
            return View(N);
        }
    }
}