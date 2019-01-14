using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_health_careModels
{
    public class AreaModel
    {

        [Display(Name = "Select Area")]
        public int AreaId { get; set; }
        [Required(ErrorMessage = "Must Enter AreaName")]
        [Display(Name = "Select AreaName")]
        public string AreaName { get; set; }
        [Display(Name = "Select City")]
        public int CityId { get; set; }
        [Display(Name = "Select Pincode")]
        public int PinCode { get; set; }

        public int Insert() { return 0; }
        public int Update() { return 0; }
        public int Delete() { return 0; }
    }
}