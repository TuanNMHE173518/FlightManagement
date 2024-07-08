using BussinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FlightRepository : IFlightRepository
    {
        public void AddFlight(Flight flight)
        {
            FlightDAO.AddFlight(flight);
        }

        public void DeleteFlight(int id)
        {
            FlightDAO.DeleteFlight(id);

        }

        public List<Flight> GetAllFlights()
        {
            return  FlightDAO.GetAllFlights();
        }

        public Flight GetFlightById(int id)
        {
            return  FlightDAO.GetFlightById(id);

        }

        public void UpdateFlight(Flight flight)
        {
            FlightDAO.UpdateFlight(flight);

        }
    }
}
