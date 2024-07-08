using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObjects
{
    public class BaggageDTO
    {
        public int Id { get; set; }

        public string? PassengerName { get; set; }
        public int? BookingId { get; set; }

        public decimal? WeightInKg { get; set; }
    }
}
