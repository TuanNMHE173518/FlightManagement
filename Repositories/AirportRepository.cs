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
        public List<Airport> GetAllAirports()
        {
            return AirportDAO.GetAirports();
        }
    }
}
