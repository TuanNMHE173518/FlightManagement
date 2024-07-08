using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BaggageService : IBaggageService
    {
        private readonly IBaggageRepository baggageRepository;
        private readonly IBookingRepository bookingRepository;
        private readonly IPassengerRepository passengerRepository;

        public BaggageService()
        {
            bookingRepository = new BookingRepository();
            passengerRepository = new PassengerRepository();
            baggageRepository = new BaggageRepository();
        }

        public void AddBaggage(Baggage baggage)
        {
            baggageRepository.AddBaggage(baggage);
        }

        public void DeleteBaggage(Baggage baggage)
        {
            baggageRepository.DeleteBaggage(baggage);
        }

        public Baggage GetBaggageById(int id)
        {
            return baggageRepository.GetBaggageById(id);
        }

        public List<BaggageDTO> GetBaggagesByBookingID(int bookingID)
        {
            var foundBaggage = from baggage in baggageRepository.GetAllBaggages()
                               join booking in bookingRepository.GetAll() on baggage.BookingId equals booking.Id 
                               join passenger in passengerRepository.GetAll() on booking.PassengerId equals passenger.Id
                               where booking.Id == bookingID
                               select new BaggageDTO()
                               {
                                   Id = baggage.Id,
                                   BookingId =baggage.BookingId,
                                   PassengerName = passenger.FirstName + " " + passenger.LastName,
                                   WeightInKg = baggage.WeightInKg
                               };
            return foundBaggage.ToList();
        }

        public void UpdateBaggage(Baggage baggage)
        {
            baggageRepository.UpdateBaggage(baggage);
        }
    }
}
