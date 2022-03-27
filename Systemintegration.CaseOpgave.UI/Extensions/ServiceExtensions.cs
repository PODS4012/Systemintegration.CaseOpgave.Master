using Systemintegration.CaseOpgave.Service;
using Systemintegration.CaseOpgave.Service.Contracts;

namespace Systemintegration.CaseOpgave.UI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureService(this IServiceCollection services) 
        {
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IPowerGeneratorService, PowerGeneratorService>();
            services.AddMemoryCache();
        }
    }
}
