namespace PruebaNewshore.Interfaces;

public interface IFlightRepository
{
    Task<List<Flight>> GetFlights(int id);
}
