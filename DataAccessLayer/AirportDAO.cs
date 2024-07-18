using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DataAccessLayer
{
    public class AirportDAO
    {
        public static List<Airport> GetAirports()
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Airports.ToList();
        }

        public static Airport GetAirportById(int id)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Airports.Find(id);
        }

        public static Airport GetAirportByCode(string code)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Airports.FirstOrDefault(x => x.Code == code);
        }

        public static void CreateAirport(Airport airport)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            var maxId = context.Airports.Max(x => x.Id);
            airport.Id = maxId + 1;
            context.Airports.Add(airport);
            context.SaveChanges();
        }

        public static void UpdateAirport(Airport airport)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            context.Airports.Update(airport);
            context.SaveChanges();
        }

        public static void DeleteAirport(Airport airport)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            List<Flight> flightsArr = FlightDAO.GetFlightsByAirportId(airport.Id);
            foreach (Flight flight in flightsArr)
            {
                if (flight.ArrivingAirport == airport.Id)
                {
                    flight.ArrivingAirport = null;
                }
                if (flight.DepartingAirport == airport.Id)
                {
                    flight.DepartingAirport = null;
                }
                flight.Status = false;
                FlightDAO.UpdateFlight(flight);
            }

            context.Airports.Remove(airport);
            context.SaveChanges();
        }
        public static List<Airport> FilterAirports(string name, string country, string state, string city)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            var airports = context.Airports.ToList();
            var temp = context.Airports.ToList();
            if (!string.IsNullOrEmpty(name))
            {
                airports = temp.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
                temp = airports;
            }
            if (!string.IsNullOrEmpty(country))
            {
                airports = temp.Where(x => x.Country.ToLower().Contains(country.ToLower())).ToList();
                temp = airports;
            }
            if (!string.IsNullOrEmpty(state))
            {
                airports = temp.Where(x => x.State.ToLower().Contains(state.ToLower())).ToList();
                temp = airports;
            }
            if (!string.IsNullOrEmpty(city))
            {
                airports = temp.Where(x => x.City.ToLower().Contains(city.ToLower())).ToList();
            }
            return airports;
        }
    }
}
