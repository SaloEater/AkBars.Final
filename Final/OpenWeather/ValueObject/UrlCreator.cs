using OpenWeather.Dto;
using WeatherClient.Dto;

namespace OpenWeather.ValueObject
{
    public class UrlCreator
    {
        public static string CreateGetUri(string cityName, OpenWeatherAPI openWeatherAPI, string metric = TemperatureMetric.CELSIUS)
        {
            return $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={openWeatherAPI.Key}&units={ConvertMetrics(metric)}";
        }

        public static string CreateFutureGetUri(string cityName, OpenWeatherAPI openWeatherAPI, string metric = TemperatureMetric.CELSIUS)
        {
            return $"https://api.openweathermap.org/data/2.5/forecast?q={cityName}&appid={openWeatherAPI.Key}&units={ConvertMetrics(metric)}";
        }


        private static string ConvertMetrics(string metric)
        {
            return metric == TemperatureMetric.CELSIUS ? "metric" : "imperial";
        }
    }
}
