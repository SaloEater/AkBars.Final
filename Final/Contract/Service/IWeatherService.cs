using Request.Dto;
using System.Threading.Tasks;
using WeatherClient.Dto.Response;

namespace Contract.Service
{
    public interface IWeatherService
    {
        public Task<TemperatureResponse> Temperature(TemperatureRequest temperature);

        public Task<WindDirectionResponse> Wind(WindDirectionRequest windDirection);

        public Task<FutureResponse[]> Future(FutureRequest future);
    }
}
