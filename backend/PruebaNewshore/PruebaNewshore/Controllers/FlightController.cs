using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaNewshore.Services;

namespace PruebaNewshore.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FlightController : Controller
{
    private readonly FlightService flightService;

    public FlightController(FlightService flightService)
    {
        this.flightService = flightService;
    }

    [HttpGet("trip-route")]
    public async Task<IActionResult> GetTripRoute(string origin, string destination,int typeFlight=0)
    {
        if(origin == destination)
        {
            return BadRequest(new{message="El origen y el destino no pueden ser iguales"});
        }
        if(origin.Length != 3 || destination.Length != 3)
        {
            return BadRequest(new{message="El origen y el destino deben tener 3 caracteres"});
        }
        List<Flight> flights= new List<Flight>();
        if(typeFlight ==0) {
            flights = await this.flightService.GetTripRoute(origin, destination,typeFlight);
        }
        if(typeFlight ==1) {
            flights = await this.flightService.GetTripRoute(origin, destination,typeFlight,1);
        }
        if(typeFlight == 2)
        {
            flights = await this.flightService.GetTripRoute(origin, destination, typeFlight,1);
            if (flights.Count == 0)
            {
                return NotFound(new{message="Su consulta no puede ser procesada"});
            }
            List<Flight> returnFlight = await this.flightService.GetTripRoute(destination, origin, typeFlight,1);
            if (returnFlight.Count == 0)
            {
                return NotFound(new{message="Su consulta no puede ser procesada"});
            }
            flights.AddRange(returnFlight);
            
        }
        if (flights.Count == 0)
        {
            return NotFound(new{message="Su consulta no puede ser procesada"});
        }
        Journey journey     = new Journey();
        journey.Origin      = origin;
        journey.Destination = destination;
        journey.AddFlights(flights);
        return Ok(new{journey});
    }
}
