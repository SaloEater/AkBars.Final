using Contract.Service;
using Request.Dto;
using WeatherClient.Dto;

namespace Service.Service
{
    public class ValidationService : IValidationService
    {
        public bool ValidateFuture(FutureRequest request)
        {
            return IsValidCityName(request.CityName) && IsValidMetric(request.Metric);
        }
        
        public bool ValidateTemperature(TemperatureRequest request)
        {

            return IsValidCityName(request.CityName) && IsValidMetric(request.Metric);
        }

        public bool ValidateWind(WindDirectionRequest request)
        {

            return IsValidCityName(request.CityName);
        }

        private bool IsValidCityName(string cityName)
        {
            return !string.IsNullOrEmpty(cityName);
        }

        private bool IsValidMetric(string metric)
        {
            return metric == TemperatureMetric.CELSIUS || metric == TemperatureMetric.FAHRENHEIT;
        }
    }
}
