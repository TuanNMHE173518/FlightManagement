using BussinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPFProject2.Resource
{
    public class EmptySeatsConverter : IValueConverter
    {

        private readonly IBookingService bookingService;
        private readonly IFlightService flightService;

        public EmptySeatsConverter()
        {
            bookingService = new BookingService();
            flightService = new FlightService();
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is int flightId)
            {
                Flight flight = flightService.GetFlightById(flightId);
                int numberSeats = bookingService.CountNumberBookingByFlightId(flightId);
                return flight.NumberPassengers - numberSeats;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
