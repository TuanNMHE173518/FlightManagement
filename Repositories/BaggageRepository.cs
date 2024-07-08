using BussinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BaggageRepository : IBaggageRepository
    {
        public void AddBaggage(Baggage baggage)
        {
            BaggageDAO.addBaggage(baggage);
        }

        public void DeleteBaggage(Baggage baggage)
        {
            BaggageDAO.deleteBaggage(baggage);
        }

        public List<Baggage> GetAllBaggages()
        {
            return BaggageDAO.GetAllBaggages();
        }

        public Baggage GetBaggageById(int id)
        {
            return BaggageDAO.GetBaggageById(id);
        }

        public void UpdateBaggage(Baggage baggage)
        {
            BaggageDAO.updateBaggage(baggage);
        }
    }
}
