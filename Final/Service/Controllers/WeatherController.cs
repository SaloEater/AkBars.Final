using Contract.Service;
using Microsoft.AspNetCore.Mvc;
using Request.Dto;
using System;
using System.Threading.Tasks;
using WeatherClient;
using WeatherClient.Dto.Response;

namespace Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase, IWeatherClient
    {
        private IWeatherService WeatherService;
        private IValidationService ValidationService;

        public WeatherController(IWeatherService weatherService, IValidationService validationService)
        {
            WeatherService = weatherService;
            ValidationService = validationService;
        }

        [HttpGet("/weather/{cityName}/future/{metric}")]
        public async Task<FutureResponse[]> future(string cityName, string metric)
        {
            var request = new FutureRequest() { CityName = cityName, Metric = metric };
            if (!ValidationService.ValidateFuture(request)) {
                throw new ArgumentException();
            }

            return await WeatherService.Future(request);
        }

        [HttpGet("/weather/temperature/{cityName}/{metric}")]
        public async Task<TemperatureResponse> temperature(string cityName, string metric)
        {
            var request = new TemperatureRequest() { CityName = cityName, Metric = metric };
            if (!ValidationService.ValidateTemperature(request)) {
                throw new ArgumentException();
            }

            return await WeatherService.Temperature(request);
        }

        [HttpGet("/weather/wind/{cityName}")]
        public async Task<WindDirectionResponse> wind(string cityName)
        {
            var request = new WindDirectionRequest() { CityName = cityName };
            if (!ValidationService.ValidateWind(request)) {
                throw new ArgumentException();
            }

            return await WeatherService.Wind(request);
        }
    }
}
