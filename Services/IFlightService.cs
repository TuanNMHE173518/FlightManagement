using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IFlightService
    {
        List<Flight> GetAllFlights();
        List<Flight> FindByAirlineAirportAnddate(int departureAirportId,int ArrivalAirportId, int airlineId, DateTime? departureDate, DateTime? arrivalDate, string status);
        List<Flight> FindByAirlineAirportAnddate2(int departureAirportId, int airlineId, DateTime? departureDate);
        void AddFlight(Flight flight);
        void UpdateFlight(Flight flight);
        void DeleteFlight(int id);
        Flight GetFlightById(int id);

        void UpdateFlightStatus(Flight flight, bool status);

        
    }
}
