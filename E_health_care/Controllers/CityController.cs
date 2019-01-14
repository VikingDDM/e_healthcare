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
    public class CityController : Controller 
    {
         E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();

        public ActionResult Index()
        {
            var dblist = db.tblCities.ToList();
            List<CityModel> list = new List<CityModel>();

            foreach(tblCity C in dblist)
            {
                CityModel D = new CityModel();
                D.CityId = C.intCityId ;
                D.CityName = C.strCityName ;
                list.Add(D);
            }
            return View(list);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
         
         public ActionResult Create(CityModel C)
        {
            tblCity ci = new tblCity();

            ci.intCityId  = C.CityId;
            ci.strCityName = C.CityName;

            db.tblCities.Add(ci);
            db.SaveChanges();
             
            return RedirectToAction("Index");

        }


        public ActionResult Details(int? id)
        {
            tblCity C = db.tblCities.SingleOrDefault(x => x.intCityId == id);

            CityModel N = new CityModel()
            {
                CityId = C.intCityId,
                CityName = C.strCityName,
            };
            return View(N);

        }
        public ActionResult Delete(int? id)
        {
            tblCity C = db.tblCities.SingleOrDefault(x => x.intCityId == id);
            CityModel N = new CityModel()
            {
                CityId = C.intCityId,
                CityName = C.strCityName,
            };
            return View(N);

            
        }
        [HttpPost]
        public ActionResult Delete(CityModel N)
        {

            var d = db.tblCities.SingleOrDefault(x => x.intCityId == N.CityId);
            db.tblCities.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(CityModel cm)
        {
            tblCity c = db.tblCities.SingleOrDefault(x => x.intCityId == cm.CityId);


            if (c != null)
            {
                c.intCityId = cm.CityId;
                c.strCityName = cm.CityName;


                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            tblCity C = db.tblCities.SingleOrDefault(x => x.intCityId == id);

            CityModel N = new CityModel()
            {
                CityId = C.intCityId,
                CityName = C.strCityName,
            };
            return View(N);
        }


     
        }
    }