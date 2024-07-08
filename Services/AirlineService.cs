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
    }
}
