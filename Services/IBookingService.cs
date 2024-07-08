using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBookingService
    {
        void DeleteBooking(int id);
        void AddBooking(Booking booking);
        List<BookingInfoDTO> GetBookingInfos();
        List<BookingInfoDTO> GetPersonalBookingInfor(int passengerId);

        List<Booking> GetAllBookings();
        List<BookingInfoDTO> FindByAirlineAirportAnddate(string departureAirportCode, string ArrivalAirportCode, string airlineCode, DateTime? departureDate, DateTime? arrivalDate, DateTime? bookingTime, string name, bool status);
        Booking GetBookingById(int id);

        void UpdateBooking(Booking booking);
        List<BookingInfoDTO> FindPersonalBookingByAirlineAirportAnddate(string departureAirportCode, string ArrivalAirportCode, string airlineCode, DateTime? departureDate, DateTime? arrivalDate, DateTime? bookingTime, bool status, int passengerId);

    }
}
