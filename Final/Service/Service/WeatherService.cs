using OpenWeather.Dto;
using Contract.Service;
using Request.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherClient.Dto.Response;
using System.Net.Http;
using System.Text.Json;
using OpenWeather.Dto.Response;
using Service.ValueObject;
using OpenWeather;
using WeatherClient.Dto;

namespace Service.Service
{
    public class WeatherService : IWeatherService
    {
        private OpenWeatherAPI OpenWeatherAPI;
        private OpenWeatherClient OpenWeatherClient;

        public WeatherService(OpenWeatherAPI openWeatherAPI, OpenWeatherClient openWeatherClient)
        {
            OpenWeatherAPI = openWeatherAPI;
            OpenWeatherClient = openWeatherClient;
        }

        public async Task<TemperatureResponse> Temperature(TemperatureRequest request)
        {
            var responseContent = await GetResponseContent(request.CityName, request.Metric);
            var data = JsonSerializer.Deserialize<Data>(responseContent, new JsonSerializerOptions() { IncludeFields = true });
            return new TemperatureResponse()
            {
                city = request.CityName,
                metric = request.Metric,
                temperature = data.Temperature.Value
            };
        }

        public async Task<WindDirectionResponse> Wind(WindDirectionRequest request)
        { 
            var responseContent = await GetResponseContent(request.CityName);
            var data = JsonSerializer.Deserialize<Data>(responseContent, new JsonSerializerOptions() { IncludeFields = true });
            return new WindDirectionResponse()
            {
                city = request.CityName,
                speed = data.Wind.Speed,
                direction = ValueObject.Direction.DetermineDirectionLabel(data.Wind.Degree)
            };
        }

        public async Task<FutureResponse[]> Future(FutureRequest request)
        {
            var responseContent = await OpenWeatherClient.forecast(request.CityName, OpenWeatherAPI.Key, ConvertMetrics(request.Metric));
            var data = JsonSerializer.Deserialize<Forecast>(responseContent, new JsonSerializerOptions() { IncludeFields = true });


            int index = 0;
            #region skip today
            do {
                var todayDate = DateTime.Now;
                var forecastData = data.datas[index];
                var date = ConvertFromUnixTimestamp(forecastData.Timestamp);
                if (date.Date > todayDate.Date) {
                    break;
                }
                index++;
            } while (true);
            #endregion

            float temperature = 0;
            int counter = 0;
            int maxIndex = data.datas.Length;
            var futureResponses = new List<FutureResponse>();

            var nextDay = ConvertFromUnixTimestamp(data.datas[index].Timestamp);
            do {
                var forecastData = data.datas[index];
                var date = ConvertFromUnixTimestamp(forecastData.Timestamp);
                if (counter > 0 && date.Date > nextDay.Date) {
                    futureResponses.Add(new FutureResponse()
                    {
                        date = nextDay.ToShortDateString(),
                        city = request.CityName,
                        metric = request.Metric,
                        temperature = temperature / counter
                    });
                    nextDay = date;
                    temperature = 0;
                    counter = 0;
                }
                temperature += forecastData.Temperature.Value;
                counter++;
            } while (++index < maxIndex);

            if (counter > 0) {
                futureResponses.Add(new FutureResponse()
                {
                    date = nextDay.ToShortDateString(),
                    city = request.CityName,
                    metric = request.Metric,
                    temperature = temperature / counter
                });
            }

            return futureResponses.ToArray();
        }

        private async Task<string> GetResponseContent(string cityName, string metric = TemperatureMetric.CELSIUS)
        {
            /*var client = new HttpClient();
            var response = await client.GetAsync(UrlCreator.CreateGetUri(cityName, OpenWeatherAPI));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();*/
            return await OpenWeatherClient.temperature(cityName, OpenWeatherAPI.Key, ConvertMetrics(metric));
        }

        private static string ConvertMetrics(string metric)
        {
            return metric == TemperatureMetric.CELSIUS ? "metric" : "imperial";
        }

        static DateTime ConvertFromUnixTimestamp(int timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}
