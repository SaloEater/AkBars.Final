using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace OpenWeather.Configuration
{
    public static class OpenWeatherExtension
    {
        public const string OPEN_WEATHER_URL = "OpenWeatherUrl";

        public static void AddOpenWeatherClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddTransient(_ => {
                var key = configuration.GetSection(OPEN_WEATHER_URL).Value;
                if (string.IsNullOrEmpty(key)) {
                    throw new KeyNotFoundException();
                }
                return RestService.For<OpenWeatherClient>(key);
            });
        }
    }
}
