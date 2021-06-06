using OpenWeather.Dto.Response;
using Refit;
using System.Threading.Tasks;

namespace OpenWeather
{
    public interface OpenWeatherClient
    {

        [Get("/weather?q={cityName}&appid={key}&units={metric}")]
        public Task<string> temperature([Query] string cityName, [Query] string key, [Query] string metric);

        [Get("/forecast?q={cityName}&appid={key}&units={metric}")]
        public Task<string> forecast([Query] string cityName, [Query] string key, [Query] string metric);
    }
}
