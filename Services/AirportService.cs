
using BussinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository airportRepository;
        public AirportService()
        {
            airportRepository = new AirportRepository();
        }

        public void CreateAirport(Airport airport)
        {
            airportRepository.CreateAirport(airport);
        }

        public void DeleteAirport(Airport airport)
        {
            airportRepository.DeleteAirport(airport);
        }

        public List<Airport> FilterAirport(string name, string country, string state, string city)
        {
            return airportRepository.FilterAirport(name, country, state, city);
        }

        public Airport GetAirportByCode(string code)
        {
            return airportRepository.GetAirportByCode(code);
        }

        public Airport GetAirportById(int id)
        {
            return airportRepository.GetAirportById(id);
        }

        public List<Airport> GetAllAirports()
        {
            return airportRepository.GetAllAirports();
        }

        public void UpdateAirport(Airport airport)
        {
            airportRepository.UpdateAirport(airport);
        }
    }
}
