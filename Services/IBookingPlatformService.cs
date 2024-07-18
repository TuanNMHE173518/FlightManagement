using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookingPlatformService
    {
        List<BookingPlatform> GetAll();
        BookingPlatform GetBookingPlatformById(int id);
        BookingPlatform GetBookingPlatformByName(string name);
        BookingPlatform GetBookingPlatformByUrl(string url);
        void AddPlatform(BookingPlatform bookingPlatform);
        void UpdatePlatform(BookingPlatform bookingPlatform);
        void DeletePlatform(BookingPlatform bookingPlatform);
        IEnumerable<BookingPlatform> GetFilterPlatform(string name, string url);
        bool IsPlatformNameExists(string name, string url, int id);
    }
}
