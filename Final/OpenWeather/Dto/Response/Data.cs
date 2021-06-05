using System.Text.Json.Serialization;

namespace OpenWeather.Dto.Response
{
    public class Data
    {
        [JsonPropertyName("main")]
        public Temperature Temperature;

        [JsonPropertyName("wind")]
        public Wind Wind;
    }
}
