using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_health_care.Models
{
    public class BookModel
    {
        [Required(ErrorMessage = "Must Enter CityID")]
        [Display(Name ="Select city")]
       
        public int CityID { get; set; }
        [Required(ErrorMessage = "Must Enter AreaID")]
        [Display(Name = "Select Area")]
        public int AreaID { get; set; }
        [Display(Name = "Select Doctor")]
        [Required(ErrorMessage = "Must Enter DoctorID")]
        public int DoctorID { get; set; }
        [Display(Name = "Select Clinic")]
        public int ClinicID { get; set; }

        public DateTime DtmDate { get; set; }
        [Display(Name = "Select Slot")]
        public int SlotID { get; set; }
        [Display(Name = "Select Member")]
        public int MemberID { get; set; }

        [Required(ErrorMessage = "Must Enter PatientName")]
        public string PatientName { get; set; }
        [Display(Name = "Select PatientAge")]
        public int PatientAge { get; set; }

        public string PatientGender { get; set; }

        public string Notes { get; set; }

        public bool IsNewCase { get; set; }

        public string OldCaseNo { get; set; }

        public DateTime AppotimentTakenTime { get; set; }
    }
}