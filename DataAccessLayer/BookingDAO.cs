using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BookingDAO
    {
        public static void AddBooking(Booking booking)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            var maxId = context.Bookings.Max(b => b.Id);
            booking.Id = maxId + 1;
            context.Bookings.Add(booking);
            context.SaveChanges();
        }

        public static List<Booking> GetAllBooking()
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Bookings.ToList();
        }

        public static void DeleteBooking(int id)
        {

            FlightManagementDbContext context = new FlightManagementDbContext();
            using(var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var booking = context.Bookings.Include(b => b.Baggages).FirstOrDefault(b => b.Id == id);
                    if(booking != null)
                    {
                        context.Baggages.RemoveRange(booking.Baggages);
                        context.Bookings.Remove(booking);
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
        

        public static void UpdateBooking(Booking booking)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            
            context.Bookings.Update(booking);
            context.SaveChanges();
        }

        public static Booking GetBookingById(int id) {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.Bookings.Where(b => b.Id == id).Include(b => b.Passenger).Include(b => b.Flight).First();


        }


        public static void UpdateRange(List<Booking> bookings)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            context.Bookings.UpdateRange(bookings);
            context.SaveChanges();
        }
    }
}
