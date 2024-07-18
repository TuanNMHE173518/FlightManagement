using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class FlightDAO
    {

        public static List<Flight> GetAllFlights()
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Flights.Include(p => p.ArrivingAirportNavigation)
                .Include(p => p.DepartingAirportNavigation)
                .Include(p => p.Airline).ToList();
        }

        public static void UpdateFlight(Flight flight)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            context.Flights.Update(flight);
            context.SaveChanges();
        }

        public static void DeleteFlight(int id)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var flight = context.Flights.Include(f => f.Bookings).ThenInclude(b => b.Baggages).FirstOrDefault(f => f.Id == id);

                    if(flight != null)
                    {
                        foreach(var booking in flight.Bookings)
                        {
                            context.Baggages.RemoveRange(booking.Baggages);
                        }

                        context.Bookings.RemoveRange(flight.Bookings);
                        context.Flights.Remove(flight);
                        context.SaveChanges();
                        transaction.Commit();
                    }

                }
                catch(Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }


                
        }

        public static void AddFlight(Flight flight)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            var maxId = context.Flights.Max(f => f.Id);
            flight.Id = maxId + 1;
            context.Flights.Add(flight);
            context.SaveChanges();
        }

        public static Flight GetFlightById(int id)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Flights.Find(id);
            
        }


        public static List<Flight> GetFlightsByAirportId(int airportId)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Flights.Where(x => x.ArrivingAirport == airportId || x.DepartingAirport == airportId).ToList();
        }

    }
}
