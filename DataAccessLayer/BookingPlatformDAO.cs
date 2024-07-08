using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BookingPlatformDAO
    {
        public static List<BookingPlatform> GetAllBookingPlatforms()
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.BookingPlatforms.ToList();

        }

        public static BookingPlatform GetBookingPlatformById(int id)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.BookingPlatforms.Find(id);
        }
    }
}
