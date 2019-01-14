using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_health_care.Models
{
    public class MemberRegisterModel
    {
        public E_health_careModels.LoginModel login { get; set; }
        public MemberModel member { get; set; }

        public Gender memgender { get; set; }
        public enum Gender { Male,Female}
    }
}