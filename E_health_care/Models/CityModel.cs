using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_health_careModels
{
    public class CityModel
    {
        public int CityId { get; set; }
        [Required(ErrorMessage = "Must Enter  CityName")]
        public string CityName { get; set; }
    }
}