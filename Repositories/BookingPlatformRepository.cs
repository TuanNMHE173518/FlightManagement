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
        public List<BookingPlatform> GetAll()
        {
            return BookingPlatformDAO.GetAllBookingPlatforms();
        }

        public BookingPlatform GetBookingPlatformById(int id)
        {
            return BookingPlatformDAO.GetBookingPlatformById(id);
        }
    }
}
