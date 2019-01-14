using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_health_careModels
{
    public class LoginModel
    {
        public int Loginid { get; set; }
        [Required(ErrorMessage = "Must Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Must Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Must Enter UserType")]
        public string UserType { get; set; }
    }

    }
