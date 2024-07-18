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
        public static void AddAirline(Airline airline)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            int id = context.Airlines.Max(x => x.Id);
            airline.Id = id + 1;
            context.Airlines.Add(airline);
            context.SaveChanges();
        }
        public static void UpdateAirline(Airline airline)
        {

            FlightManagementDbContext context = new FlightManagementDbContext();
            context.Airlines.Update(airline);
            context.SaveChanges();

        }
        public static void DeleteAirline(Airline airline)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            context.Airlines.Remove(airline);
            context.SaveChanges();
        }
        public static Airline? GetAirlineById(int id)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Airlines.Find(id);
        }
    }
}