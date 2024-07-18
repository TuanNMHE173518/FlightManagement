using BussinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public static BookingPlatform GetBookingPlatformByName(string name)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.BookingPlatforms.FirstOrDefault(x => !string.IsNullOrEmpty(x.Name) && x.Name == name);
        }
        public static BookingPlatform GetBookingPlatformByUrl(string url)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            return context.BookingPlatforms.FirstOrDefault(x => !string.IsNullOrEmpty(x.Url) && x.Url == url);
        }

        public static void AddPlatform(BookingPlatform bookingPlatform)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            var bookingid = context.BookingPlatforms.Max(x => x.Id);
            bookingPlatform.Id = bookingid + 1;
            context.BookingPlatforms.Add(bookingPlatform);
            context.SaveChanges();
        }

        public static void UpdatePlatform(BookingPlatform bookingPlatform)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            context.BookingPlatforms.Update(bookingPlatform);
            context.SaveChanges();
        }

        public static void DeletePlatform(BookingPlatform bookingPlatform)
        {
            FlightManagementDbContext context = new FlightManagementDbContext();
            context.BookingPlatforms.Remove(bookingPlatform);
            context.SaveChanges();
        }

    }
}