using E_health_care;
using E_health_care;
using E_health_careModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_health_care_Controllers
{
   
    public class AreaController : Controller
    {
         E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();
        public ActionResult Index()
        {
           var dblist = db.tblAreas.ToList();
            List<AreaModel> list = new List<AreaModel>();

            foreach (tblArea A in dblist)
            {
                AreaModel ar = new AreaModel();
                ar.AreaId = A.intAreaId;
                ar.AreaName = A.strAreaName ;
                ar.CityId = (int)A.intCityId ;
                ar.PinCode = (int)A.intPinCode ;

                list.Add(ar);
            }
                return View(list);
        } 
             
       
        public ActionResult Create()
        {
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
        public ActionResult Create(AreaModel A)
        {
            tblArea ar = new tblArea();
            ar.intAreaId = A.AreaId;
            ar.strAreaName = A.AreaName;
            ar.intCityId = A.CityId;
            ar.intPinCode = A.PinCode;


            db.tblAreas.Add(ar);
            db.SaveChanges();    
           
            return RedirectToAction("Index");
        }

        public ActionResult Details(int ?id)
        {
            tblArea A = db.tblAreas.SingleOrDefault(x => x.intAreaId == id);
            AreaModel N = new AreaModel()
            {
              AreaId = A.intAreaId,
              AreaName = A.strAreaName,
              CityId =(int) A.intCityId,
              PinCode =(int) A.intPinCode,

            };
            return View(N);
            
        }
        public ActionResult Delete(int ?id)
        {
            tblArea A = db.tblAreas.SingleOrDefault(x => x.intAreaId == id);
            AreaModel N = new AreaModel()
            {
                AreaId = A.intAreaId,
                AreaName = A.strAreaName,
                CityId = (int)A.intCityId,
                PinCode = (int)A.intPinCode,

            };
            return View(N);
           
        }
        [HttpPost]
        public ActionResult Delete(AreaModel N)
        {

            var d = db.tblAreas.SingleOrDefault(x => x.intAreaId == N.AreaId);
            db.tblAreas.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(AreaModel ar)
        {
            tblArea a = db.tblAreas.SingleOrDefault(x => x.intAreaId == ar.AreaId);


            if (a != null)
            {
                a.intAreaId = ar.AreaId;
                a.strAreaName = ar.AreaName;
                a.intCityId = ar.CityId;
                a.intPinCode = ar.PinCode;

               
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            tblArea A = db.tblAreas.SingleOrDefault(x => x.intAreaId == id);
            AreaModel N = new AreaModel()
            {
                AreaId = A.intAreaId,
                AreaName = A.strAreaName,
                CityId = (int)A.intCityId,
                PinCode = (int)A.intPinCode,

            };
            return View(N);
        }
    }
}