namespace PruebaNewshore;
public class Journey:Trip
{
    private List<Flight> flights;
    public IReadOnlyCollection<Flight> Flights { 
        get { return flights.AsReadOnly(); }
    }

    public Journey()
    {
        this.flights = new List<Flight>();
    }
    public void AddFlights(List<Flight> flights)
    {  
        this.flights=flights;
        CalculatePrice();
    }
    public void CalculatePrice()
    {
        this.Price = this.flights.Sum(f => f.Price);
    }
}
