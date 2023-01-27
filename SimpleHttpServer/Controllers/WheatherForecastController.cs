using HttpServer;

namespace SimpleHttpServer.Controllers
{
    public record Temperature(int temperature, int humidity);

    public class WheatherForecastController : IController
    {
        public Temperature[] Index()
        {
            return new[]
            {
                new Temperature(12, 80),
                new Temperature(15, 65),
                new Temperature(19, 55)
            };
        }

        public async Task<Temperature> GetCurrentWheatherForecast()
        {
            return new Temperature(22, 70);
        }
    }
}