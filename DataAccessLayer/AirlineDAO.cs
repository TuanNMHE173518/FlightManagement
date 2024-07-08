using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class AirlineDAO
    {
        public static List<Airline> GetAirlines()
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Airlines.ToList();
        }
    }
}
