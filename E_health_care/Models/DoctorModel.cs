using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_health_care.Models
{
    public class DoctorModel
    {

        public int DoctorID { get; set; }
        public int LoginID { get; set; }
        [Required(ErrorMessage = "Must Enter Doctor Name")]
        public string DoctorName { get; set; }
        public int TypeID { get; set; }

        [Required(ErrorMessage = "Must Enter Details")]
        public string Details { get; set; }
        public string Photo { get; set; }

    }


    }

    
