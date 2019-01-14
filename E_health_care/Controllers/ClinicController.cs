using E_health_care.Models;
using E_health_careModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_health_care.Controllers
{

    public class ClinicController : Controller
    {
         E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();
        public ActionResult Index()
        {
            var dblist = db.tblClinics.ToList();
            List<ClinicModel> list = new List<ClinicModel>();

            foreach (tblClinic C in dblist)
            {
                ClinicModel cl = new ClinicModel();
                cl.ClinicId = C.intClinicID;

                cl.ClinicName = C.strClinicName;

                cl.AreaId = (int)C.intAreaID;

                cl.Address = C.strAddress;

                cl.PhoneNo = C.strPHoneNo;

                list.Add(cl);
            }

            return View(list);

        }

        public ActionResult Create()
        {
            //City List
            var dblist = db.tblCities.ToList();
            List<CityModel> list = new List<CityModel>();

            List<SelectListItem> citylist = new List<SelectListItem>();
            citylist.Add(new SelectListItem() { Text = "Select City", Value = "-1" });
            foreach (tblCity C in dblist)
            {
                citylist.Add(new SelectListItem() { Text = C.strCityName, Value = C.intCityId + "" });
            }

            ViewData["citylist"] = citylist;


            return View();
        }

        [HttpPost]
        public ActionResult Create(ClinicModel C)
        {
            tblClinic cl = new tblClinic();
            cl.intClinicID = C.ClinicId;

            cl.strClinicName = C.ClinicName;

            cl.intAreaID = C.AreaId;

            cl.strAddress = C.Address;

            cl.strPHoneNo = C.PhoneNo;

            db.tblClinics.Add(cl);
            db.SaveChanges();


            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            tblClinic C = db.tblClinics.SingleOrDefault(x => x.intClinicID == id);
            ClinicModel N = new ClinicModel()
            {
                ClinicId = C.intClinicID,
                ClinicName = C.strClinicName,
                AreaId = (int)C.intAreaID,
                Address = C.strAddress,
                PhoneNo = C.strPHoneNo,
            };
            return View(N);


        }
        public ActionResult Delete(int? id)
        {
            tblClinic C = db.tblClinics.SingleOrDefault(x => x.intClinicID == id);
            ClinicModel N = new ClinicModel()
            {
                ClinicId = C.intClinicID,
                ClinicName = C.strClinicName,
                AreaId = (int)C.intAreaID,
                Address = C.strAddress,
                PhoneNo = C.strPHoneNo,
            };
            return View(N);

        }
        [HttpPost]
        public ActionResult Delete(ClinicModel N)
        {

            var d = db.tblClinics.SingleOrDefault(x => x.intClinicID == N.ClinicId);
            db.tblClinics.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(ClinicModel cm)
        {
            tblClinic c = db.tblClinics.SingleOrDefault(x => x.intClinicID == cm.ClinicId);


            if (c != null)
            {
                c.intClinicID = cm.ClinicId;
                c.strClinicName = cm.ClinicName;
                c.intAreaID = cm.AreaId;
                c.strAddress = cm.Address;
                c.strPHoneNo = cm.PhoneNo;

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            tblClinic C = db.tblClinics.SingleOrDefault(x => x.intClinicID == id);
            ClinicModel N = new ClinicModel()
            {
                ClinicId = C.intClinicID,
                ClinicName = C.strClinicName,
                AreaId = (int)C.intAreaID,
                Address = C.strAddress,
                PhoneNo = C.strPHoneNo,
            };
            return View(N);

        }


        public JsonResult GetAreas(string id)
        {

            int sid = Convert.ToInt32(id);
            var list = db.tblAreas.Where(x => x.intCityId == sid).ToList();

            List<SelectListItem> arealist = new List<SelectListItem>();
            foreach (tblArea C in list)
            {
                arealist.Add(new SelectListItem() { Text = C.strAreaName, Value = C.intAreaId + "" });
            }


            return Json(new SelectList(arealist, "Value", "Text"));
        }

    }
}