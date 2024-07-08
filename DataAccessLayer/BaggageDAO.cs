using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BaggageDAO
    {

        public static List<Baggage> GetAllBaggages()
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Baggages.ToList();
        }

        public static void addBaggage(Baggage baggage)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            var maxId = context.Baggages.Max(b => b.Id);
            baggage.Id = maxId+1;
            context.Baggages.Add(baggage);
            context.SaveChanges();

        }

        public static void updateBaggage(Baggage baggage)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            context.Baggages.Update(baggage);
            context.SaveChanges();
        }

        public static void deleteBaggage(Baggage baggage)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            context.Baggages.Remove(baggage);
            context.SaveChanges();
        }

        public static Baggage GetBaggageById(int id) 
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Baggages.Find(id);
        }
    }
}
