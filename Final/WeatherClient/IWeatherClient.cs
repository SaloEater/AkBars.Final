using Refit;
using System.Threading.Tasks;
using WeatherClient.Dto.Response;

namespace WeatherClient
{
    public interface IWeatherClient
    {
        [Get("/weather/temperature/{cityName}/{metric}")]
        public Task<TemperatureResponse> temperature([Query]string cityName, [Query] string metric);

        [Get("/weather/wind/{cityName}")]
        public Task<WindDirectionResponse> wind([Query] string cityName);

        [Get("/weather/{cityName}/future/{metric}")]
        public Task<FutureResponse[]> future([Query] string cityName, [Query] string metric);
    }
}
