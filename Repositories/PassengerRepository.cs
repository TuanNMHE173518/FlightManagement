using BussinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        public void AddPassenger(Passenger passenger)
        {
             PassengerDAO.AddPassenger(passenger);
        }

        public void AddRange(List<Passenger> passengers)
        {
            PassengerDAO.AddRange(passengers);
        }

        public List<Passenger> GetAll()
        {
            return PassengerDAO.GetAllPassengers();
        }

        public Passenger GetPassengerByEmail(string email)
        {
            return PassengerDAO.GetPassengerByEmail(email);
        }

        public Passenger GetPassengerById(int id)
        {
            return PassengerDAO.GetPassengerById(id);
        }

        public void UpdatePassenger(Passenger passenger)
        {
            PassengerDAO.UpdatePassenger(passenger);
        }
    }
}
