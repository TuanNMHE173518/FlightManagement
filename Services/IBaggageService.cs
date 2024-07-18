using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IBaggageService
    {

        List<BaggageDTO> GetBaggagesByBookingID(int bookingID);
        void AddBaggage(Baggage baggage);
        void UpdateBaggage(Baggage baggage);
        void DeleteBaggage(Baggage baggage);
        Baggage GetBaggageById(int id);


    }
}
