using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository passengerRepository;

        public PassengerService()
        {
            passengerRepository = new PassengerRepository();    
        }
        public void AddPassengers(Passenger passenger)
        {
            passengerRepository.AddPassenger(passenger);
        }

        public List<Passenger> FindPassengersByNameEmailAndAge(string name, string email, int ageFrom, int ageTo)
        {
            var currentDate = DateOnly.FromDateTime(DateTime.Now);

            var foundPassengers = GetAll()
                .Where(p => (string.IsNullOrEmpty(name) || (p.FirstName + " " + p.LastName).ToLower().Contains(name.ToLower()))
                            && (string.IsNullOrEmpty(email) || p.Email.ToLower().Contains(email.ToLower()))
                            && (ageFrom == 0 || (p.DateOfBirth.HasValue && GetAge(p.DateOfBirth.Value, currentDate) >= ageFrom))
                            && (ageTo == 0 || (p.DateOfBirth.HasValue && GetAge(p.DateOfBirth.Value, currentDate) <= ageTo))
                           )
                .ToList();

            return foundPassengers;
        }

        private int GetAge(DateOnly birthDate, DateOnly currentDate)
        {
            int age = currentDate.Year - birthDate.Year;
            if (birthDate > currentDate.AddYears(-age))
                age--;
            return age;
        }

        public List<Passenger> GetAll()
        {
            return passengerRepository.GetAll();
        }

        public Passenger GetPassengerByEmail(string email)
        {
            return passengerRepository.GetPassengerByEmail(email);
        }

        public Passenger GetPassengerById(int id)
        {
            return passengerRepository.GetPassengerById(id);
        }

        public void UpdatePassenger(Passenger passenger)
        {
            passengerRepository.UpdatePassenger(passenger);
        }

        public void AddRange(List<Passenger> passengers)
        {
            passengerRepository.AddRange(passengers);
        }
    }
}
