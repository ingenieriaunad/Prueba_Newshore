using Newtonsoft.Json;
using PruebaNewshore.Interfaces;
namespace PruebaNewshore.Repository
{
    public class FlightRepository: IFlightRepository
    {
        private readonly HttpClient httpClient;

        public FlightRepository()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://recruiting-api.newshore.es/");
        }
        public async Task<List<Flight>> GetFlights(int id)
        {
            var response = await httpClient.GetAsync($"api/flights/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                List<FlightData>? data = JsonConvert.DeserializeObject<List<FlightData>>(json);
                if (data == null)
                {
                    return new List<Flight>();
                }
                var flights = from flightData in data
                              select new Flight
                              {
                                  Transport   = 
                                  { 
                                    FlightCarrier = flightData.FlightCarrier, 
                                    FlightNumber  = flightData.FlightNumber
                                  },
                                  Origin      = flightData.DepartureStation,
                                  Destination = flightData.ArrivalStation,
                                  Price       = flightData.Price
                              };
                return flights.ToList();
            }
            return new List<Flight>();
        }
    }
}
