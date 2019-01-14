using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_health_care.Models
{
    public class DoctorTimeModel
    {
        public int TimeId { get; set; }

        public int DoctorId { get; set; }

        public int ClinicId { get; set; }

        public int DayNo { get; set; }

        public DateTime FromTime { get; set; }

        public DateTime ToTime { get; set; }

        public decimal Charges { get; set; }
    }
}