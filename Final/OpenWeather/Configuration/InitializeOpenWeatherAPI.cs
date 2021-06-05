using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenWeather.Dto;
using System.Collections.Generic;

namespace OpenWeather.Configuration
{
    public static class InitializeOpenWeatherAPI
    {
        public static void AddOpenWeatherAPI(
            this IServiceCollection services,
            IConfiguration configuration
        ) {
            services.TryAddSingleton(_ => {
                var key = configuration.GetSection(OpenWeatherAPI.KEY).Value;
                if (string.IsNullOrEmpty(key)) {
                    throw new KeyNotFoundException();
                }
                return new OpenWeatherAPI() { Key = key };
            });
        }
    }
}
