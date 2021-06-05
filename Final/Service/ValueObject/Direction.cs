using System;

namespace Service.ValueObject
{
    public class Direction
    {

        public static string DetermineDirectionLabel(float direction)
        {
            if (Compare(direction, 360, 337.5) || Compare(direction, 22.5, 0) || direction == 0) {
                return WeatherClient.Dto.Direction.N;
            } else if (Compare(direction, 67.5, 22.5)) {
                return WeatherClient.Dto.Direction.NE;
            } else if (Compare(direction, 112.5, 67.5)) {
                return WeatherClient.Dto.Direction.E;
            } else if (Compare(direction, 157.5, 112.5)) {
                return WeatherClient.Dto.Direction.SE;
            } else if (Compare(direction, 202.5, 157.5)) {
                return WeatherClient.Dto.Direction.S;
            } else if (Compare(direction, 247.5, 202.5)) {
                return WeatherClient.Dto.Direction.SW;
            } else if (Compare(direction, 292.5, 247.5)) {
                return WeatherClient.Dto.Direction.W;
            } else if (Compare(direction, 337.5, 292.5)) {
                return WeatherClient.Dto.Direction.NW;
            }

            throw new ArgumentOutOfRangeException();
        }

        private static bool Compare(float desired, double upper, double lower)
        {
            return desired <= upper && desired > lower;
        }
    }
}
