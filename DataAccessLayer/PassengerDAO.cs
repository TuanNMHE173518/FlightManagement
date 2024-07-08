using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class PassengerDAO
    {

        public static void AddPassenger(Passenger passenger)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();    
            var maxId = context.Passengers.Max(p => p.Id);
            passenger.Id = maxId + 1;
            context.Passengers.Add(passenger);
            context.SaveChanges();
        }
        public static List<Passenger> GetAllPassengers()
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Passengers.ToList();
        }

        public static Passenger GetPassengerByEmail(string email)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Passengers.FirstOrDefault(p => p.Email.ToLower().Equals(email.ToLower()));

        }

        public static Passenger GetPassengerById(int id)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Passengers.Find(id);

        }

        public static void UpdatePassenger(Passenger passenger)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            context.Passengers.Update(passenger);
            context.SaveChanges();
        }

        public static void AddRange(List<Passenger> passengers)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            context.Passengers.AddRange(passengers);
            context.SaveChanges();
        }

    }

    
}
