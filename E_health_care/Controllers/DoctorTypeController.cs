using E_health_careModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_health_care.Controllers
{
    public class DoctorTypeController : Controller
    {
         E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();

        public int TypeID { get; private set; }
        public string strType { get; private set; }

        public ActionResult Index()
        {
            var dblist = db.tblDoctorTypes.ToList();
            List<DoctortypeModel> list = new List<DoctortypeModel>();

            foreach (tblDoctorType D in dblist)
            {
                DoctortypeModel dt = new DoctortypeModel();
                dt.TypeId = D.intTypeId;
                dt.Type = D.strType;
                list.Add(dt);
            }
                return View(list);
            }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Create(DoctortypeModel D)
        {
            tblDoctorType dt = new tblDoctorType();

            dt.intTypeId = D.TypeId;
            dt.strType = D.Type;

            db.tblDoctorTypes.Add(dt);     
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Details(int? id)
        {

            tblDoctorType D = db.tblDoctorTypes.SingleOrDefault(x => x.intTypeId == id);
            DoctortypeModel N = new DoctortypeModel()
            {
              TypeId = D.intTypeId,
              Type = D.strType,
            };
            return View(N);

        }
        public ActionResult Delete(int? id)
        {
            tblDoctorType D = db.tblDoctorTypes.SingleOrDefault(x => x.intTypeId == id);
            DoctortypeModel N = new DoctortypeModel()
            {
                TypeId = D.intTypeId,
                Type = D.strType,
                
            };
            return View(N);
        }
        [HttpPost]
        public ActionResult Delete(DoctortypeModel N)
        {

            var d = db.tblDoctorTypes.SingleOrDefault(x => x.intTypeId == N.TypeId);
            db.tblDoctorTypes.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(DoctortypeModel dt)
        {
            tblDoctorType d = db.tblDoctorTypes.SingleOrDefault(x => x.intTypeId == dt.TypeId);


            if (d != null)
            {
                d.intTypeId = dt.TypeId;
                d.strType = dt.Type;
                
              


                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            tblDoctorType D = db.tblDoctorTypes.SingleOrDefault(x => x.intTypeId == id);
            DoctortypeModel N = new DoctortypeModel()
            {
                TypeId = D.intTypeId,
                Type = D.strType,

            };
            return View(N);
        }

    }
}