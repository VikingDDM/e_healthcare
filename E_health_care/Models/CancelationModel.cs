using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_health_care.Models
{
    public class CancelationModel
    {
      public  int CancelationID { get; set; }
      public  int AppointmentID { get; set; }

        public int CancelByLoginID { get; set; }
        public string Reason { get; set; }

        public DateTime CancelDateTime { get; set; }
    }
}