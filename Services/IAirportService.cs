using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAirportService
    {
        List<Airport> GetAllAirports();
        void CreateAirport(Airport airport);
        void UpdateAirport(Airport airport);
        void DeleteAirport(Airport airport);
        Airport GetAirportById(int id);
        Airport GetAirportByCode(string code);
        List<Airport> FilterAirport(string name, string country, string state, string city);
    }
}
