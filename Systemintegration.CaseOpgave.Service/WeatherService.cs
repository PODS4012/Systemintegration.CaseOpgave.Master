using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systemintegration.CaseOpgave.Service.Contracts;
using Systemintegration.CaseOpgave.Shared.DataTransferObjects;
using VisualCrossingsService;

namespace Systemintegration.CaseOpgave.Service
{
    public sealed class WeatherService : IWeatherService
    {
        private readonly IMemoryCache memoryCache;
        private readonly IOptions<SecretSettings> options;

        public WeatherService(IMemoryCache memoryCache, IOptions<SecretSettings> options)
        {
            this.memoryCache = memoryCache;
            this.options = options;
        }
        public IEnumerable<WeatherParam> GetWeatherParams()
        {
            string key = this.options.Value.WeatherServiceKey;

            IEnumerable<WeatherParam> returnResult;

            returnResult = this.memoryCache.Get<IEnumerable<WeatherParam>>("weatherCacheKey");

            if (returnResult is null)
            {
                ForecastServiceClient client = new ForecastServiceClient();

                var result = client.GetForecastAsync("Kolding", key).Result.Body.GetForecastResult;

                var forcasts = result.location.values;
                if (forcasts is not null)
                {
                    returnResult = forcasts.Select(val => new WeatherParam()
                    {
                        DateTime = val.datetimeStr,
                        Temp = val.temp,
                        Conditions = val.conditions
                    }).Where(val => val.DateTime < DateTime.Now.AddHours(5));

                    this.memoryCache.Set("weatherCacheKey", returnResult, TimeSpan.FromMinutes(1));
                }
                else
                {
                    var defaultForcast = result.location.currentConditions;

                    var mappedDefaultForcast = new WeatherParam()
                    {
                        DateTime = defaultForcast.datetime,
                        Temp = defaultForcast.temp,
                        Conditions = "No Data"
                    };

                    returnResult = new[] { mappedDefaultForcast };

                    this.memoryCache.Set("weatherCacheKey", returnResult, TimeSpan.FromMinutes(1));
                }
            }

            return returnResult;
        }
    }
}
