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
        public List<Airport> GetAllAirports()
        {
            return airportRepository.GetAllAirports();
        }
    }
}
