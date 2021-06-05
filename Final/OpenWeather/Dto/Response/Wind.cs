using System.Text.Json.Serialization;

namespace OpenWeather.Dto.Response
{
    public class Wind
    {
        [JsonPropertyName("speed")]
        public float Speed;

        [JsonPropertyName("deg")]
        public float Degree;
    }
}
