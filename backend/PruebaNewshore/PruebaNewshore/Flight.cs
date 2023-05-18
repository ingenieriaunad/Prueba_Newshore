using Newtonsoft.Json;
namespace PruebaNewshore;
//clase todo de la agregacion entre Flight y Transport
public class Flight:Trip
{
    private Transport transport;
    public Transport Transport {
        get { return transport; }
    }
    public Flight()
    {
        this.transport = new Transport();
    }
}
