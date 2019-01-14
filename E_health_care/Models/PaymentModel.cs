using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_health_care.Models
{
    public class PaymentModel
    {
        public int PaymentID { get; set; }
        public int AppoitmentID { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }

    }
}