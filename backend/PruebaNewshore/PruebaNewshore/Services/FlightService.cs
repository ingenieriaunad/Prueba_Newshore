using PruebaNewshore.Interfaces;

namespace PruebaNewshore.Services
{
    public class FlightService
    {
        private readonly IFlightRepository flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            this.flightRepository = flightRepository;
        }

        public async Task<List<Flight>> GetTripRoute(string origin, string destination, int route , int type=0)
        {
            List<Flight> flights = await this.flightRepository.GetFlights(route); 
            return FindTravelRoute(origin, destination, flights,type);
        }

        private List<Flight> FindTravelRoute(string origin, string destination, List<Flight> flights, int type=0)
        {
            // type 0: Viaje directo
            // type 1: Viaje con escala
            List<Flight> travelRoute = new List<Flight>();
            if(type == 0 )
            {
                IEnumerable<Flight> query = from flight in flights
                                            where flight.Origin == origin && flight.Destination == destination
                                            select flight;
                Flight? directFlight = query.FirstOrDefault();
                if (directFlight!=null)
                {
                    travelRoute.Add(directFlight);
                }
                return travelRoute;
            }
            List<Flight> flightsOrigin = flights.Where(f => f.Origin == origin).ToList();
            if (type == 1)
            {
                foreach (Flight flight in flightsOrigin)
                {
                    if (flight.Destination == destination)
                    {
                        travelRoute.Add(flight);
                        return travelRoute;
                    }
                    else
                    {
                        string newOrigin = flight.Destination;
                        List<Flight> newFlights = flights.Where(f => f.Origin == newOrigin).ToList();
                        List<Flight> newTravelRoute = FindTravelRoute(newOrigin, destination, newFlights, 1);
                        if (newTravelRoute.Count > 0)
                        {
                            travelRoute.Add(flight);
                            travelRoute.AddRange(newTravelRoute);
                            return travelRoute;
                        }
                    }
                }
            }
            return travelRoute;        
        }
    }
}
