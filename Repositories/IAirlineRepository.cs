using BussinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAirlineRepository
    {
        List<Airline> GetAll();
        void AddAirline(Airline airline);
        void UpdateAirline(Airline airline);
        void DeleteAirline(Airline airline);
        Airline? GetAirlineById(int id);
    }
}