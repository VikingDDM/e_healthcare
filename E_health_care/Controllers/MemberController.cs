using E_health_care.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_health_care.Controllers
{
    public class MemberController : Controller
    {
         E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();

        public ActionResult RegisterMember()
        {
            return View();
        }
        public ActionResult Index()
        {
            var dblist = db.tblMembers.ToList();
            List<MemberModel> list = new List<MemberModel>();

            foreach (tblMember M in dblist)
            {
                MemberModel me = new MemberModel();
                me.MemberID = M.intMemberID;
                me.LoginID = (int)M.intLoginID;
                me.MemberName = M.strMemberName;
                me.Age = (int)M.intAge;
                me.Address = M.strAddress;
                me.PhoneNo = M.strPhoneNo;
                me.Gender = M.strGender; 

                list.Add(me);
            }
            return View(list);
            
        }
        public ActionResult Create()
        {

            //
            List<SelectListItem> gender = new List<SelectListItem>();
            gender.Add(new SelectListItem() { Text = "Male", Value = "Male" });
            gender.Add(new SelectListItem() { Text = "Female", Value = "Female" });
            ViewData["gender"] = gender;

            return View();
        }

        [HttpPost]
        public ActionResult Create(MemberRegisterModel Mr)
        {
            MemberModel M = Mr.member;
            E_health_careModels.LoginModel L = Mr.login;

            L.UserType = "Member";


            tblLogin ln = new tblLogin();

            ln.intLoginid = L.Loginid;
            ln.strEmail = L.Email;
            ln.strPassword = L.Password;
            ln.strUserType = L.UserType;


            db.tblLogins.Add(ln);
            db.SaveChanges();

            tblMember me = new tblMember();
            me.intMemberID = M.MemberID;
            me.intLoginID = ln.intLoginid;
            me.strMemberName = M.MemberName;
            me.intAge = M.Age;
            me.strAddress = M.Address;
            me.strPhoneNo = M.PhoneNo;
            me.strGender = M.Gender;


            db.tblMembers.Add(me);
            db.SaveChanges();

            return RedirectToAction("Authenticate","Login");
        }

        public ActionResult Details(int? id)
        {
            tblMember M = db.tblMembers.SingleOrDefault(x => x.intMemberID == id);
            MemberModel P = new MemberModel() { MemberID = M.intMemberID, LoginID = (int)M.intLoginID, MemberName = M.strMemberName, Age = (int)M.intAge, Address = M.strAddress, PhoneNo = M.strPhoneNo,
            Gender = M.strGender};
            return View(P);
        }
        public ActionResult Delete(int? id)
        {
            tblMember M = db.tblMembers.SingleOrDefault(x => x.intMemberID == id);
            MemberModel P = new MemberModel()
            {
                MemberID = M.intMemberID,
                LoginID = (int)M.intLoginID,
                MemberName = M.strMemberName,
                Age = (int)M.intAge,
                Address = M.strAddress,
                PhoneNo = M.strPhoneNo,
                Gender = M.strGender
            };
            return View(P);
        }
        [HttpPost]
        public ActionResult Delete(MemberModel P)
        {

            var d = db.tblMembers.SingleOrDefault(x => x.intMemberID == P.MemberID);
            db.tblMembers.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(MemberModel mm)
        {
            tblMember m = db.tblMembers.SingleOrDefault(x => x.intMemberID == mm.MemberID);


            if (m != null)
            {
                m.intMemberID = mm.MemberID;
                m.intLoginID = mm.LoginID;
                m.strMemberName = mm.MemberName;
                m.intAge = mm.Age;
                m.strAddress = mm.Address;
                m.strPhoneNo = mm.PhoneNo;
                m.strGender = mm.Gender;


                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            tblMember M = db.tblMembers.SingleOrDefault(x => x.intMemberID == id);
            MemberModel P = new MemberModel()
            {
                MemberID = M.intMemberID,
                LoginID = (int)M.intLoginID,
                MemberName = M.strMemberName,
                Age = (int)M.intAge,
                Address = M.strAddress,
                PhoneNo = M.strPhoneNo,
                Gender = M.strGender
            };
            return View(P);
        }
    }
}