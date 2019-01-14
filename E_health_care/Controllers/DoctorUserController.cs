using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_health_care.Controllers
{
    public class DoctorUserController : Controller
    {
        // GET: DoctorUser
        public ActionResult Index()
        {
            if (Session["doctorid"] == null)
            {
                return RedirectToAction("Authenticate", "Login");
            }


            return View();
        }
    }
}