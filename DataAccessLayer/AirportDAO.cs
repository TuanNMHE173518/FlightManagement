using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class AirportDAO
    {
        public static List<Airport> GetAirports()
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Airports.ToList();
        }
    }
}
