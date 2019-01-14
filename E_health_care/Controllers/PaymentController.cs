using E_health_care.Models;
using E_health_care;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_health_care.Controllers
{
    public class PaymentController : Controller
    {
         E_health_care.e_healthcare_dbEntities db = new  E_health_care.e_healthcare_dbEntities();
        public ActionResult Index()
        {
            var dblist = db.tblPayments.ToList();
            List<PaymentModel> list = new List<PaymentModel>();

            foreach (tblPayment P in dblist)
            {
                PaymentModel py = new PaymentModel();

                py.PaymentID = P.intPaymentID;
                py.AppoitmentID =(int)P.intAppoitmentID;
                py.Amount = (decimal)P.decAmount;
                py.Status = P.strStatus;
              
                list.Add(py);
            }
            return View(list);
        }




        public ActionResult Create()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult Create(PaymentModel P)
        {
            tblPayment pa = new tblPayment();
            pa.intPaymentID = P.PaymentID;
            pa.intAppoitmentID = P.AppoitmentID;
            pa.decAmount = P.Amount;
            pa.strStatus = P.Status;

            db.tblPayments.Add(pa);
            db.SaveChanges();


            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id) 
        {
            tblPayment P = db.tblPayments.SingleOrDefault(x => x.intPaymentID == id);

            PaymentModel M = new PaymentModel() { PaymentID = P.intPaymentID , Amount =(decimal) P.decAmount , AppoitmentID =(int) P.intAppoitmentID , Status =P.strStatus  };
            return View(M);
        }
        public ActionResult Delete(int? id)
        {
            tblPayment P = db.tblPayments.SingleOrDefault(x => x.intPaymentID == id);
            PaymentModel M = new PaymentModel() { PaymentID = P.intPaymentID, Amount = (decimal)P.decAmount, AppoitmentID = (int)P.intAppoitmentID, Status = P.strStatus };
            return View(M);
        }
        [HttpPost]
        public ActionResult Delete(PaymentModel M)
        {

            var d = db.tblPayments.SingleOrDefault(x => x.intPaymentID == M.PaymentID);
            db.tblPayments.Remove(d);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(PaymentModel pm)
        {
            tblPayment p = db.tblPayments.SingleOrDefault(x => x.intPaymentID == pm.PaymentID);


            if (p != null)
            {
                p.intPaymentID = pm.PaymentID;
                p.decAmount = pm.Amount;
                p.intAppoitmentID = pm.AppoitmentID;
                p.strStatus = pm.Status;
                
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            tblPayment P = db.tblPayments.SingleOrDefault(x => x.intPaymentID == id);
            PaymentModel M = new PaymentModel() { PaymentID = P.intPaymentID, Amount = (decimal)P.decAmount, AppoitmentID = (int)P.intAppoitmentID, Status = P.strStatus };
            return View(M);
        }

    }
}