namespace Request.Dto
{
    public record FutureRequest
    {
        public string CityName { get; init; }

        public string Metric { get; init; }
    }
}
