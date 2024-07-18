using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAirlineService
    {
        List<Airline> GetAllAirlines();
        void AddAirline(Airline airline);
        void RemoveAirline(Airline airline);
        void UpdateAirline(Airline airline);
        Airline? GetAirlineById(int id);
        IEnumerable<Airline> GetFilteredAirlines(string code, string name, string country);
    }
}
