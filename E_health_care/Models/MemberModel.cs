using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_health_care.Models
{
    public class MemberModel
    {
       public int MemberID { get; set; }

       public int LoginID { get; set; }
        [Required(ErrorMessage ="Must Enter Member Name")]
       
        public string MemberName { get; set; }
        [Required(ErrorMessage = "Age")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Must Enter Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]

        public string PhoneNo { get; set; }

       public string Gender { get; set; }
    
    }
}