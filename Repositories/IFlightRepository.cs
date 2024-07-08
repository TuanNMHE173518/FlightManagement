using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IFlightRepository
    {

        List<Flight> GetAllFlights();

        void AddFlight(Flight flight);
        void UpdateFlight(Flight flight);
        void DeleteFlight(int id);
        Flight GetFlightById(int id);
    }
}
