using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_health_care.Models
{
    public class AppointmentSlotModel
    {
        [Display(Name = "Select Slot")]
        public int SlotID { get; set; }
        [Display(Name = "Select Doctor")]
        public int DoctorID { get; set; }
        [Display(Name = "Select Clinic")]
        public int ClinicID { get; set; }

      
        public DateTime Date { get; set; }

      
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Display(Name = "Select Status")]
        public string  Status { get; set; }
    }
}