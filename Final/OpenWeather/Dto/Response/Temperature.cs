using System.Text.Json.Serialization;

namespace OpenWeather.Dto.Response
{
    public class Temperature
    {
        [JsonPropertyName("temp")]
        public float Value;
    }
}
