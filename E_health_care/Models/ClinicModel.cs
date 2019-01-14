using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_health_care.Models
{
    public class ClinicModel
    {
       public int ClinicId { get; set; }
        [Required(ErrorMessage = "Must Enter  ClinicName")]
        public string ClinicName { get; set; }
      
       public int AreaId { get; set; }
        [Required(ErrorMessage = "Must Enter Address")]
        public string Address { get; set; }
        
       public string PhoneNo { get; set; }    
    }
}
