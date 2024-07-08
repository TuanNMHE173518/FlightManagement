using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPassengerRepository
    {
        void AddPassenger(Passenger passenger);

        List<Passenger> GetAll();

        Passenger GetPassengerByEmail(string email);
        Passenger GetPassengerById(int id);
        void UpdatePassenger(Passenger passenger);
        void AddRange(List<Passenger> passengers);
    }
}
