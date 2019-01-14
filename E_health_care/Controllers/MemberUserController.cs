using E_health_care.Models;
using E_health_careModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace E_health_care.Controllers
{

    public class MemberUserController : Controller
    {
         E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();

        // GET: MemberUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BookAppoitment(BookModel M)
        {

            tblAppointment ap = new tblAppointment();
            ap.intSlotID = M.SlotID;
            ap.intMemberID = Convert.ToInt32 (Session["memberid"]);// Session here
            ap.intPatientAge = M.PatientAge;
            ap.strNotes = M.Notes;
            ap.strOldCaseNo = M.OldCaseNo;
            ap.strPatientGender = M.PatientGender;
            ap.strPatientName = M.PatientName;
            ap.dtmAppotimentTakenTime = DateTime.Now;
            ap.blnIsNewCase = M.IsNewCase;

            db.tblAppointments.Add(ap);
            db.SaveChanges();

            tblAppointmentSlot s = db.tblAppointmentSlots.SingleOrDefault(x => x.intSlotID == M.SlotID);
            if(s != null)
            {
                s.strStatus = "Booked";
                db.SaveChanges();


                //Mail Code Here
                String em = Session["email"].ToString();
                String sub = "Your Appointment Booked";

                String det = "\nAppointment ID " + ap.intAppointmentID;
                det += "\nFor :" + ap.strPatientName;


                SmtpClient cl = new SmtpClient("smtp.gmail.com", 587);
                cl.Credentials = new NetworkCredential("fake74053@gmail.com", "fake123456");
                cl.EnableSsl = true;

                MailMessage mg = new MailMessage("fake74053@gmail.com", em, sub, det);
                cl.Send(mg);
                //If you get error in mail comment cl.send line


            }
            return RedirectToAction("Index");
        }
        public ActionResult BookAppoitment()
        {
            if (Session["usertype"] == null)
            {
                Response.Redirect("../Login/Authenticate");
            }

            var dblist = db.tblCities.ToList();
            List<CityModel> list = new List<CityModel>();

            List<SelectListItem> citylist = new List<SelectListItem>();
            citylist.Add(new SelectListItem() { Text = "Select City", Value = "-1" });
            foreach (tblCity C in dblist)
            {
                citylist.Add(new SelectListItem() { Text=C.strCityName , Value = C.intCityId+"" });
            }

            ViewData["citylist"] = citylist;

            //
            List<SelectListItem> gender = new List<SelectListItem>();
            gender.Add(new SelectListItem() {Text="Male",Value="Male" });
            gender.Add(new SelectListItem() { Text = "Female", Value = "Female" });
            ViewData["gender"] = gender;

            //Doctor Type

            var dbtypelist = db.tblDoctorTypes.ToList();
            List<DoctortypeModel> clist = new List<DoctortypeModel>();

            List<SelectListItem> DoctorTypelist = new List<SelectListItem>();
            DoctorTypelist.Add(new SelectListItem() { Text = "Select DoctorType", Value = "-1" });
            foreach (tblDoctorType C in dbtypelist)
            {
                DoctorTypelist.Add(new SelectListItem() { Text = C.strType, Value = C.intTypeId + "" });
            }

            ViewData["doctortypelist"] = DoctorTypelist;
            
            //Slot List

            var slist = db.tblAppointmentSlots.Where(x=> x.strStatus == "Available").ToList();

            List<SelectListItem> timelist = new List<SelectListItem>();
            foreach (tblAppointmentSlot C in slist)
            {
                timelist.Add(new SelectListItem() { Text = C.dtmStartTime + "-" + C.dtmEndTime, Value = C.intSlotID + "" });
            }

            timelist.Add(new SelectListItem() { Text = "11:00" + "-" + "12:00", Value = "1" });

            ViewData["timelist"] = timelist;
            //Ends


            var dbdlist = db.tblDoctors.ToList();
            List<DoctortypeModel> dlist = new List<DoctortypeModel>();

           

            var dbalist = db.tblClinics.ToList();
            List<ClinicModel> alist = new List<ClinicModel>();

            List<SelectListItem> cliniclist = new List<SelectListItem>();
            cliniclist.Add(new SelectListItem() { Text="Select Clinic",Value="-1"});
            foreach (tblClinic C in dbalist)
            {
                cliniclist.Add(new SelectListItem() { Text = C.strClinicName, Value = C.intClinicID + "" });
            }

            ViewData["cliniclist"] = cliniclist;



            return View();
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


        public JsonResult GetDoctors(string id)
        {

            int sid = Convert.ToInt32(id);
            var list = db.tblDoctors.Where(x => x.intTypeID == sid).ToList();

            List<SelectListItem> doctorlist = new List<SelectListItem>();
            doctorlist.Add(new SelectListItem() { Text = "Select Doctor", Value =  "-1" });

            foreach (tblDoctor C in list)
            {
                doctorlist.Add(new SelectListItem() { Text = C.strDoctorName, Value = C.intDoctorID + "" });
            }


            return Json(new SelectList(doctorlist, "Value", "Text"));
        }



        public JsonResult GetClinics(string id)
        {

            int sid = Convert.ToInt32(id);
            var list = db.tblClinics.ToList();

            List<SelectListItem> cliniclist = new List<SelectListItem>();
            cliniclist.Add(new SelectListItem() { Text = "Select Clinic", Value = "-1" });

            foreach (tblClinic C in list)
            {
                cliniclist.Add(new SelectListItem() { Text = C.strClinicName, Value = C.intClinicID + "" });
            }


            return Json(new SelectList(cliniclist, "Value", "Text"));
        }


        public JsonResult GetTimeSlot(string id)
        {

            int sid = Convert.ToInt32(id);
            var list = db.tblAppointmentSlots.Where(x=> (x.intDoctorID == sid && x.strStatus == "Available")).ToList();

            List<SelectListItem> timelist = new List<SelectListItem>();
            timelist.Add(new SelectListItem() { Text = "Select Date and Time Slot", Value = "-1" });

            foreach (tblAppointmentSlot C in list)
            {
                DateTime Dt = DateTime.Now;
                if(C.dtmStartTime > Dt)
                timelist.Add(new SelectListItem() { Text = C.dtmStartTime +"-"+C.dtmEndTime, Value = C.intSlotID + "" });
               
            }

            
            return Json(new SelectList(timelist, "Value", "Text"));
        }
    }
}