
using E_health_care;
using E_health_care;
using E_health_careModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace E_health_care_Controllers
{
    public class LoginController : Controller
    {
         E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();
        public ActionResult Index()
        {
           
        
            var dblist = db.tblLogins.ToList();
            List<LoginModel> list = new List<LoginModel>();

            foreach (tblLogin ln in dblist)
            {
                LoginModel L = new LoginModel();
                L.Loginid = ln.intLoginid;
                L.Email = ln.strEmail;
                L.Password = ln.strPassword;
                L.UserType = ln.strUserType;
                list.Add(L);
            }
            return View(list);
            
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Create(LoginModel L)
        {
            tblLogin ln = new tblLogin();

            ln.intLoginid = L.Loginid;
            ln.strEmail = L.Email;
            ln.strPassword = L.Password;
            ln.strUserType = L.UserType;


            db.tblLogins.Add(ln);
            db.SaveChanges();

            return RedirectToAction("Index");


        }


        public ActionResult Details(int? id)
        {
            tblLogin L = db.tblLogins.SingleOrDefault(x => x.intLoginid == id);
            LoginModel N = new LoginModel()
            {
                Loginid = L.intLoginid,
                Email = L.strEmail,
                Password = L.strPassword,
                UserType = L.strUserType,

            };
            return View(N);
        }
        public ActionResult Delete(int? id)
        {
            tblLogin L = db.tblLogins.SingleOrDefault(x => x.intLoginid == id);
            LoginModel N = new LoginModel()
            {
                Loginid = L.intLoginid,
                Email = L.strEmail,
                Password = L.strPassword,
                UserType = L.strUserType,

            };
            return View(N);
        }
        [HttpPost]
        public ActionResult Delete(LoginModel N)
        {

            var d = db.tblLogins.SingleOrDefault(x => x.intLoginid == N.Loginid);
            db.tblLogins.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(LoginModel lm)
        {
            tblLogin l = db.tblLogins.SingleOrDefault(x => x.intLoginid == lm.Loginid);


            if (l != null)
            {
                l.intLoginid = lm.Loginid;
                l.strEmail = lm.Email;
                l.strPassword = lm.Password;
                l.strUserType = lm.UserType;


               

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            tblLogin L = db.tblLogins.SingleOrDefault(x => x.intLoginid == id);
            LoginModel N = new LoginModel()
            {
                Loginid = L.intLoginid,
                Email = L.strEmail,
                Password = L.strPassword,
                UserType = L.strUserType,

            };
            return View(N);
        }

        public ActionResult Authenticate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authenticate(LoginModel L)
        {
            tblLogin t = db.tblLogins.SingleOrDefault(x => (x.strEmail == L.Email && x.strPassword == L.Password));
            if(t != null)
            {
                if(t.strPassword == L.Password )
                {
                    Session["usertype"] = t.strUserType.ToLower();
                    Session["loginid"] = t.intLoginid;
                    Session["email"] = t.strEmail;
                        if(t.strUserType.ToLower()=="admin")
                        {
                        return RedirectToAction("Index", "Admin");
                        }
                        else if (t.strUserType.ToLower() == "doctor")
                    {

                        tblDoctor d = db.tblDoctors.SingleOrDefault(x => x.intLoginID == t.intLoginid);
                        if(d != null)
                        {
                            Session["doctorid"] = d.intDoctorID;
                            return RedirectToAction("Index", "DoctorUser");
                        }
                        else
                            ViewBag.Error = "Invalid Details";


                    }
                    else if (t.strUserType.ToLower() == "member")
                    {
                        
                        tblMember T = db.tblMembers.SingleOrDefault(x => x.intLoginID == t.intLoginid);
                        if (T != null)
                        {
                            Session["memberid"] = T.intMemberID;
                            return RedirectToAction("Index", "MemberUser");
                        }
                        else
                            ViewBag.Error = "Invalid Details";
                    }
                }
                else
                {
                    ViewBag.Error = "Invalid Details";
                }
            }
            else
            {
                ViewBag.Error = "Invalid Details";
            }
            return View();
        }

        public ActionResult Forgot()
        {
            ViewBag.msg = "";
            return View();
        }

        [HttpPost]
        public ActionResult Forgot(LoginModel L)
        {
            var lg = db.tblLogins.SingleOrDefault(x => x.strEmail == L.Email);

            if(lg==null)
            {
                ViewBag.msg = "Invalid Email";
            }
            else
            {
                String newpass = new Random().Next(1000, 9999).ToString();

                lg.strPassword = newpass;
                db.SaveChanges();
                String sub = "Your New Password for Appointment Token Generation";
                String msg = " Hello Your New Password is " + newpass;

                SmtpClient cl = new SmtpClient("smtp.gmail.com",587);
                cl.Credentials = new NetworkCredential("fake74053@gmail.com", "fake123456");
                cl.EnableSsl = true;

                MailMessage mg = new MailMessage("fake74053@gmail.com", lg.strEmail, sub, msg);
                cl.Send(mg);
                ViewBag.msg = "Your New PAssword has been sent to your Email";

            }
            return View();
        }
    }
}