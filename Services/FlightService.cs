using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository flightRepository;
        private readonly IBookingRepository bookingRepository;
        
        public FlightService()
        {
            flightRepository = new FlightRepository();
            bookingRepository = new BookingRepository();
            
        }

        public void AddFlight(Flight flight)
        {
            flightRepository.AddFlight(flight);
        }

        public void DeleteFlight(int id)
        {
            flightRepository.DeleteFlight(id);

        }

        public List<Flight> FindByAirlineAirportAnddate(int departureAirportId, int ArrivalAirportId, int airlineId, DateTime? departureDate, DateTime? arrivalDate, string status)
        {
            var foundFlight = GetAllFlights().Where(f => (departureAirportId == 0 || f.DepartingAirport == departureAirportId )
                                                        && (ArrivalAirportId ==0 ||  f.ArrivingAirport == ArrivalAirportId)
                                                        && (airlineId == 0 ||  f.AirlineId == airlineId)
                                                        && (departureDate == null || f.DepartureTime.Value >= departureDate)
                                                        && (arrivalDate == null || f.ArrivalTime.Value <= arrivalDate)
                                                        && (status == "All" || f.Status == bool.Parse(status))).ToList();
            return foundFlight;
        }

        public List<Flight> FindByAirlineAirportAnddate2(int departureAirportId, int airlineId, DateTime? departureDate)
        {
            var foundFlight = GetAllFlights().Where(f => (departureAirportId == 0 || f.DepartingAirport == departureAirportId)
                                                        && (airlineId == 0 || f.AirlineId == airlineId)
                                                        && (departureDate == null || f.DepartureTime.Value >= departureDate)).ToList();
            return foundFlight;
        }

        public List<Flight> GetAllFlights()
        {
            return flightRepository.GetAllFlights();
        }

        public Flight GetFlightById(int id)
        {
            return flightRepository.GetFlightById(id);

        }

        public void UpdateFlight(Flight flight)
        {
            flightRepository.UpdateFlight(flight);

        }

        public void UpdateFlightStatus(Flight flight, bool status)
        {
            flight.Status = status;
            var bookings = bookingRepository.GetAll().Where(b => b.FlightId == flight.Id).ToList();
            foreach (var booking in bookings)
            {
                booking.Status = status;
            }

            bookingRepository.UpdateRange(bookings);
            flightRepository.UpdateFlight(flight);
        }
    }
}
