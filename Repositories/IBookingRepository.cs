using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingRepository
    {
        void DeleteBooking(int id);
        void AddBooking(Booking booking);
        List<Booking> GetAll();
        Booking GetBookingById(int id);
        void UpdateBooking(Booking booking);

        void UpdateRange(List<Booking> bookings);
    }
}
