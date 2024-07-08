using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPassengerService
    {
        void AddPassengers(Passenger passenger);
        Passenger GetPassengerByEmail(string email);
        Passenger GetPassengerById(int id);
        List<Passenger> GetAll();
        void UpdatePassenger(Passenger passenger);

        List<Passenger> FindPassengersByNameEmailAndAge(string name, string email, int ageFrom, int ageTo);
        void AddRange(List<Passenger> passengers);
    }
}
