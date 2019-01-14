using E_health_care.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_health_care.Controllers
{
    public class ChargeController : Controller
    {
         E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();

        public ActionResult Index()
        {
            var dblist = db.tblCharges.ToList();
            List<ChargesModel> list = new List<ChargesModel>();

            foreach (tblCharge cr in dblist)
            {
                ChargesModel C = new ChargesModel();
                C.ChargesID = cr.intChargesID;
                C.DoctorID =(int)cr.intDoctorID;
                C.ClinicID = (int)cr.intClinicID;
                C.NewCaseCharge = (decimal)cr.decNewCaseCharge;
                C.OldCaseCharge = (decimal)cr.decOldCaseCharge;
                list.Add(C);
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

        public ActionResult Create(ChargesModel C)
        {
            tblCharge ch = new tblCharge();
            ch.intDoctorID = Convert.ToInt32 (Session["doctorid"] );
            ch.intClinicID = C.ClinicID;
            ch.decNewCaseCharge = C.NewCaseCharge;
            ch.decOldCaseCharge = C.OldCaseCharge;

            db.tblCharges.Add(ch);
            db.SaveChanges();


            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            tblCharge C = db.tblCharges.SingleOrDefault(x => x.intChargesID == id);

            ChargesModel N = new ChargesModel()
            {
                ChargesID = C.intChargesID,
                DoctorID =(int) C.intDoctorID,
                ClinicID = (int)C.intClinicID,
                NewCaseCharge = (decimal)C.decNewCaseCharge,
                OldCaseCharge = (decimal)C.decOldCaseCharge,
            
            };
            return View(N);

        }
        public ActionResult Delete(int? id)
        {
            tblCharge C = db.tblCharges.SingleOrDefault(x => x.intChargesID == id);


            ChargesModel N = new ChargesModel()
            {
                ChargesID = C.intChargesID,
                DoctorID = (int)C.intDoctorID,
                ClinicID = (int)C.intClinicID,
                NewCaseCharge = (decimal)C.decNewCaseCharge,
                OldCaseCharge = (decimal)C.decOldCaseCharge,

            };
            return View(N);
        }

        [HttpPost]
        public ActionResult Delete(ChargesModel N)
        {

            var d = db.tblCharges.SingleOrDefault(x => x.intChargesID == N.ChargesID);
            db.tblCharges.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(ChargesModel ch)
        {
            tblCharge c = db.tblCharges.SingleOrDefault(x => x.intChargesID == ch.ChargesID);


            if (c != null)
            {
                c.intChargesID = ch.ChargesID;
                c.intDoctorID = ch.DoctorID;
                c.intClinicID = ch.ClinicID;
                c.decNewCaseCharge = ch.NewCaseCharge;
                c.decOldCaseCharge = ch.OldCaseCharge;
              


                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            tblCharge C = db.tblCharges.SingleOrDefault(x => x.intChargesID == id);


            ChargesModel N = new ChargesModel()
            {
                ChargesID = C.intChargesID,
                DoctorID = (int)C.intDoctorID,
                ClinicID = (int)C.intClinicID,
                NewCaseCharge = (decimal)C.decNewCaseCharge,
                OldCaseCharge = (decimal)C.decOldCaseCharge,

            };
            return View(N);
        }
    }
}