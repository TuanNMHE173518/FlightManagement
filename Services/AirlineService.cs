using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AirlineService : IAirlineService
    {
        private readonly IAirlineRepository airlineRepository;
        public AirlineService()
        {
            airlineRepository = new AirlineRepository();
        }
        public List<Airline> GetAllAirlines()
        {
            return airlineRepository.GetAll();
        }
        public void AddAirline(Airline airline)
        {
            airlineRepository.AddAirline(airline);
        }
        public void RemoveAirline(Airline airline)
        {
            airlineRepository.DeleteAirline(airline);
        }
        public void UpdateAirline(Airline airline)
        {
            airlineRepository.UpdateAirline(airline);
        }
        public Airline? GetAirlineById(int id)
        {

            return airlineRepository.GetAirlineById(id);
        }
        public IEnumerable<Airline> GetFilteredAirlines(string code, string name, string country)
        {
            return airlineRepository.GetAll()
                .Where(a => (string.IsNullOrEmpty(code) || a.Code.ToLower().Contains(code.ToLower())) &&
                            (string.IsNullOrEmpty(name) || a.Name.ToLower().Contains(name.ToLower())) &&
                            (string.IsNullOrEmpty(country) || a.Country.ToLower().Contains(country.ToLower())));
        }
    }
}
