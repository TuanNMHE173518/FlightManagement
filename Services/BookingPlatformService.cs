using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingPlatformService : IBookingPlatformService
    {
        private readonly IBookingPlatformRepository bookingPlatformRepository;

        public BookingPlatformService()
        {
            bookingPlatformRepository = new BookingPlatformRepository();
        }
        public List<BookingPlatform> GetAll()
        {
            return bookingPlatformRepository.GetAll();
        }

        public BookingPlatform GetBookingPlatformById(int id)
        {
            return bookingPlatformRepository.GetBookingPlatformById(id);
        }
    }
}
