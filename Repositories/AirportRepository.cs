// repository
using BussinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AirportRepository : IAirportRepository
    {
        public void CreateAirport(Airport airport)
        {
            AirportDAO.CreateAirport(airport);
        }

        public void DeleteAirport(Airport airport)
        {
            AirportDAO.DeleteAirport(airport);
        }

        public List<Airport> FilterAirport(string name, string country, string state, string city)
        {
            return AirportDAO.FilterAirports(name, country, state, city);
        }

        public Airport GetAirportByCode(string code)
        {
            return AirportDAO.GetAirportByCode(code);
        }

        public Airport GetAirportById(int id)
        {
            return AirportDAO.GetAirportById(id);
        }

        public List<Airport> GetAllAirports()
        {
            return AirportDAO.GetAirports();
        }

        public void UpdateAirport(Airport airport)
        {
            AirportDAO.UpdateAirport(airport);
        }
    }
}
