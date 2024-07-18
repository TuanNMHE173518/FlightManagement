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

        public void AddPlatform(BookingPlatform bookingPlatform)
        {
            bookingPlatformRepository.AddPlatform(bookingPlatform);
        }

        public void DeletePlatform(BookingPlatform bookingPlatform)
        {
            bookingPlatformRepository.DeletePlatform(bookingPlatform);
        }

        public List<BookingPlatform> GetAll()
        {
            return bookingPlatformRepository.GetAll();
        }

        public BookingPlatform GetBookingPlatformById(int id)
        {
            return bookingPlatformRepository.GetBookingPlatformById(id);
        }

        public BookingPlatform GetBookingPlatformByName(string name)
        {
            return bookingPlatformRepository.GetBookingPlatformByName(name);
        }

        public BookingPlatform GetBookingPlatformByUrl(string url)
        {
            return bookingPlatformRepository.GetBookingPlatformByUrl(url);
        }

        public IEnumerable<BookingPlatform> GetFilterPlatform(string name, string url)
        {
            return bookingPlatformRepository.GetAll().Where(p => (string.IsNullOrEmpty(name) || p.Name.ToLower().Contains(name.ToLower())) && (string.IsNullOrEmpty(url) || (p.Url != null && p.Url.ToLower().Contains(url.ToLower()))));
        }

        public bool IsPlatformNameExists(string name, string url, int id)
        {
            return bookingPlatformRepository.GetAll().Any(b => (b.Name == name || b.Url == url) && b.Id != id);
        }

        public void UpdatePlatform(BookingPlatform bookingPlatform)
        {
            bookingPlatformRepository.UpdatePlatform(bookingPlatform);
        }
    }
}