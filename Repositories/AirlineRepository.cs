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
    }
}
