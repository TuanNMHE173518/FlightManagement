using BussinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class AirlineRepository : IAirlineRepository
    {
        public List<Airline> GetAll()
        {
            return AirlineDAO.GetAirlines();
        }
        public void AddAirline(Airline airline) => AirlineDAO.AddAirline(airline);

        public void UpdateAirline(Airline airline) => AirlineDAO.UpdateAirline(airline);

        public void DeleteAirline(Airline airline) => AirlineDAO.DeleteAirline(airline);

        public Airline? GetAirlineById(int id) => AirlineDAO.GetAirlineById(id);

    }
}