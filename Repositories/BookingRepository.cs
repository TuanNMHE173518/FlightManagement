using BussinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingRepository : IBookingRepository
    {
        public void AddBooking(Booking booking)
        {
            BookingDAO.AddBooking(booking);
        }

        public void DeleteBooking(int id)
        {
             BookingDAO.DeleteBooking(id);
        }

        public List<Booking> GetAll()
        {
            return BookingDAO.GetAllBooking();
        }

        public Booking GetBookingById(int id)
        {
            return BookingDAO.GetBookingById(id);
        }

        public void UpdateBooking(Booking booking)
        {
            BookingDAO.UpdateBooking(booking);
        }

        public void UpdateRange(List<Booking> bookings)
        {
            BookingDAO.UpdateRange(bookings);
        }
    }
}
