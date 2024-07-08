using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBaggageRepository
    {
        List<Baggage> GetAllBaggages();
        void AddBaggage(Baggage baggage);
        void UpdateBaggage(Baggage baggage);
        void DeleteBaggage(Baggage baggage);
        Baggage GetBaggageById(int id);
    }
}
