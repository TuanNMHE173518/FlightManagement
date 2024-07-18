using BussinessObjects;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBookingPlatformRepository
    {
        List<BookingPlatform> GetAll();
        BookingPlatform GetBookingPlatformById(int id);
        BookingPlatform GetBookingPlatformByName(string name);
        BookingPlatform GetBookingPlatformByUrl(string url);
        void AddPlatform(BookingPlatform bookingPlatform);
        void UpdatePlatform(BookingPlatform bookingPlatform);
        void DeletePlatform(BookingPlatform bookingPlatform);
    }
}