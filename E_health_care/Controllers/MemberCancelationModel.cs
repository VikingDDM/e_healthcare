using System;

namespace E_health_care.Controllers
{
    internal class CancelationModel
    {
        public int AppointmentID { get; internal set; }
        public int CancelationID { get; internal set; }
        public int CancelByLoginID { get; internal set; }
        public DateTime CancelDateTime { get; internal set; }
        public string Reason { get; internal set; }
    }
}