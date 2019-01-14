using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_health_care.Models
{
    public class DoctorRegister
    { 
        public E_health_care.Models.DoctorModel doctor { get; set; }
        public E_health_careModels.LoginModel login { get; set; }
    }
}