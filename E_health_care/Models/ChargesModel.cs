     
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_health_care.Models
{
    public class ChargesModel
    {
      public int ChargesID { get; set; }

      public int DoctorID { get; set; }

      public int ClinicID { get; set; }

      public decimal NewCaseCharge { get; set; }
      public decimal OldCaseCharge { get; set; }
    }
}