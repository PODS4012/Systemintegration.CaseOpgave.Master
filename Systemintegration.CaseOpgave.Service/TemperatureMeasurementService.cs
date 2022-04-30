using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systemintegration.CaseOpgave.Service.Contracts;
using Systemintegration.CaseOpgave.Service.Repository;
using Systemintegration.CaseOpgave.Shared.DataTransferObjects;

namespace Systemintegration.CaseOpgave.Service
{
    public sealed class TemperatureMeasurementService : ITemperatureMeasurementService
    {
        private readonly TemperatureMeasurementContext context;
        private readonly IMemoryCache memoryCache;

        public TemperatureMeasurementService(TemperatureMeasurementContext context, IMemoryCache memoryCache)
        {
            this.context = context;
            this.memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Temperatur>> ListAllTodayAsync()
        {
            List<Temperatur> resultSet;

            resultSet = this.memoryCache.Get<List<Temperatur>>("TemperaturCacheKey");

            if (resultSet is null)
            {
                resultSet = await context.Temperaturs.Where(x => x.Date == DateTime.Today).OrderByDescending(x => x.Time).ToListAsync();

                this.memoryCache.Set("TemperaturCacheKey", resultSet, TimeSpan.FromMinutes(1));
            }

            return resultSet;
        }
    }
}
