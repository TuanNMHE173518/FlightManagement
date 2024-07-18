using BussinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingPlatformRepository : IBookingPlatformRepository
    {
        public void AddPlatform(BookingPlatform bookingPlatform)
        {
            BookingPlatformDAO.AddPlatform(bookingPlatform);
        }

        public void DeletePlatform(BookingPlatform bookingPlatform)
        {
            BookingPlatformDAO.DeletePlatform(bookingPlatform);
        }

        public List<BookingPlatform> GetAll()
        {
            return BookingPlatformDAO.GetAllBookingPlatforms();
        }

        public BookingPlatform GetBookingPlatformById(int id)
        {
            return BookingPlatformDAO.GetBookingPlatformById(id);
        }

        public BookingPlatform GetBookingPlatformByName(string name)
        {
            return BookingPlatformDAO.GetBookingPlatformByName(name);
        }

        public BookingPlatform GetBookingPlatformByUrl(string url)
        {
            return BookingPlatformDAO.GetBookingPlatformByUrl(url);
        }

        public void UpdatePlatform(BookingPlatform bookingPlatform)
        {
            BookingPlatformDAO.UpdatePlatform(bookingPlatform);
        }
    }
}
