using System.Text.Json.Serialization;

namespace OpenWeather.Dto.Response
{
    public class Forecast
    {
        [JsonPropertyName("list")]
        public ForecastData[] datas;
    }
}
