using System.Text.Json.Serialization;

namespace OpenWeather.Dto.Response
{
    public class ForecastData
    {

        [JsonPropertyName("dt")]
        public int Timestamp { get; set; }

        [JsonPropertyName("main")]
        public Temperature Temperature;
    }
}
