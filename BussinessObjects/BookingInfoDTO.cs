using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects
{
    public class BookingInfoDTO
    {
        public int id { get; set; }
        public string PassengerName { get; set; }
        public string AirlineName { get; set; }
        public string DepartingAirportName { get; set; }

        public string ArrivingAirportName { get; set; }

        public string DepartingAirportCode { get; set; }

        public string ArrivingAirportCode { get; set; }
        public DateTime? DepartureTime { get; set; }

        public DateTime? ArrivalTime { get; set; }
        public DateTime? BookingTime { get; set; }
        public string AirlineCode { get; set; }
        public string PlatformName {  get; set; }
        public bool? Status { get; set; }
    }
}
