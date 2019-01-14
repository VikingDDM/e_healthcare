using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_health_care.Models
{
    public class AppointmentModel
    {
       public int AppointmentID { get; set; }

        [Display(Name = "Select Slot")]
        public int SlotID { get; set; }

       public int MemberID { get; set; }
        [Required(ErrorMessage = "Must Enter PatientName")]
        [Display(Name = "Enter PatientName")]
        public string PatientName { get; set; }

        [Display(Name = "Enter PatientAge")]
        public int PatientAge { get; set; }
       public string PatientGender { get; set; }
       public string Notes { get; set; }
       public bool IsNewCase { get; set; }

       public string OldCaseNo { get; set; }

       public DateTime AppotimentTakenTime { get; set; }

    
    }
}