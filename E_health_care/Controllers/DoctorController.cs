using E_health_care;
using E_health_care.Models;
using E_health_careModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_health_care.Controllers
{
    public class DoctorController : Controller
    {
         E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();
        public ActionResult Index()
        {

            var dblist = db.tblDoctors.ToList();
            List<DoctorModel> list = new List<DoctorModel>();

            foreach (tblDoctor Dr in dblist)
            {
                DoctorModel D = new DoctorModel();
                D.DoctorID = Dr.intDoctorID;
                D.LoginID = (int)Dr.intLoginID;
                D.DoctorName = Dr.strDoctorName;
                D.TypeID = (int)Dr.intTypeID;
                D.Details = Dr.strDetails;
                D.Photo = Dr.strPhoto;
                list.Add(D);
            }

                    return View(list);

            }  
        
        
        
        public ActionResult Create()
        {

            //DoctorType List

            var dbtypelist = db.tblDoctorTypes.ToList();
            List<DoctortypeModel> clist = new List<DoctortypeModel>();

            List<SelectListItem> DoctorTypelist = new List<SelectListItem>();
            DoctorTypelist.Add(new SelectListItem() { Text = "Select DoctorType", Value = "-1" });
            foreach (tblDoctorType C in dbtypelist)
            {
                DoctorTypelist.Add(new SelectListItem() { Text = C.strType, Value = C.intTypeId + "" });
            }

            ViewData["doctortypelist"] = DoctorTypelist;


            //----
            return View();
        }
        [HttpPost]

        public ActionResult Create(E_health_care.Models.DoctorRegister  Dr, HttpPostedFileBase file)
        {

            string _FileName = "";
            try
            {
                if (file.ContentLength > 0)
                {
                     _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
            
            }

            E_health_careModels.LoginModel L = Dr.login;

            L.UserType = "Doctor";


            tblLogin ln = new tblLogin();

            ln.intLoginid = L.Loginid;
            ln.strEmail = L.Email;
            ln.strPassword = L.Password;
            ln.strUserType = L.UserType;


            db.tblLogins.Add(ln);
            db.SaveChanges();

            DoctorModel D = Dr.doctor;
            
            tblDoctor dr = new tblDoctor();

            dr.intDoctorID = D.DoctorID;
            dr.intLoginID = ln.intLoginid;
            dr.strDoctorName = D.DoctorName;
            dr.intTypeID = D.TypeID;
            dr.strDetails = D.Details;
            dr.strPhoto = _FileName;

            db.tblDoctors.Add(dr);       
            db.SaveChanges();


            return RedirectToAction("Authenticate","Login");
        }
        public ActionResult Details(int? id)
        {
            tblDoctor D = db.tblDoctors.SingleOrDefault(x => x.intDoctorID == id);
            DoctorModel N = new DoctorModel()
            {
               DoctorID = D.intDoctorID,
               LoginID = (int)D.intLoginID,
               DoctorName = D.strDoctorName,
               TypeID = (int)D.intTypeID,
               Details = D.strDetails,
               Photo = D.strPhoto,
            };
            return View(N);

        }
        public ActionResult Delete(int? id)
        {
            tblDoctor D = db.tblDoctors.SingleOrDefault(x => x.intDoctorID == id);
            DoctorModel N = new DoctorModel()
            {
                DoctorID = D.intDoctorID,
                LoginID = (int)D.intLoginID,
                DoctorName = D.strDoctorName,
                TypeID = (int)D.intTypeID,
                Details = D.strDetails,
                Photo = D.strPhoto,
            };
            return View(N);

        }
        [HttpPost]
        public ActionResult Delete(DoctorModel N)
        {

            var d = db.tblDoctors.SingleOrDefault(x => x.intDoctorID== N.DoctorID);
            db.tblDoctors.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(DoctorModel dm)
        {
            tblDoctor d = db.tblDoctors.SingleOrDefault(x => x.intDoctorID== dm.DoctorID);


            if (d != null)
            {
                d.intDoctorID = dm.DoctorID;
                d.intLoginID = dm.LoginID;
                d.strDoctorName = dm.DoctorName;
                d.intTypeID = dm.TypeID;
                d.strDetails = dm.Details;
                d.strPhoto = dm.Photo;


                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            tblDoctor D = db.tblDoctors.SingleOrDefault(x => x.intDoctorID == id);
            DoctorModel N = new DoctorModel()
            {
                DoctorID = D.intDoctorID,
                LoginID = (int)D.intLoginID,
                DoctorName = D.strDoctorName,
                TypeID = (int)D.intTypeID,
                Details = D.strDetails,
                Photo = D.strPhoto,
            };
            return View(N);

        }



    }
}