using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Systemintegration.CaseOpgave.Service;
using Systemintegration.CaseOpgave.Service.Contracts;
using Systemintegration.CaseOpgave.Service.Repository;
using Systemintegration.CaseOpgave.Shared.DataTransferObjects;

namespace Systemintegration.CaseOpgave.UI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureService(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IPowerGeneratorService, PowerGeneratorService>();
            services.AddScoped<ITemperatureMeasurementService, TemperatureMeasurementService>();
            services.AddDbContext<TemperatureMeasurementContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TemperaturDbString")));
            services.Configure<SecretSettings>(configuration.GetSection("SecretSettings"));
            services.AddScoped<IElectricityPriceService, ElectricityPriceService>();
            var producerConfig = new ProducerConfig();
            var consumerConfig = new ConsumerConfig();
            configuration.Bind("Kafkaproducer", producerConfig);
            configuration.Bind("Kafkaconsumer", consumerConfig);
            services.AddSingleton<ProducerConfig>(producerConfig);
            services.AddSingleton<ConsumerConfig>(consumerConfig);
            services.AddMemoryCache();
        }
    }
}
