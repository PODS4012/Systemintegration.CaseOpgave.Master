using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systemintegration.CaseOpgave.Service.ElPriceHelper;

namespace Systemintegration.CaseOpgave.Service.DataPipelines
{
    public class ProducerWrapper
    {
        private readonly ProducerConfig config;
        private readonly string topicName;
        private readonly IProducer<Null, double> producer;

        public ProducerWrapper(ProducerConfig config, string topicName)
        {
            this.config = config;
            this.topicName = topicName;
            var producerBuilder = new ProducerBuilder<Null, double>(this.config);
            this.producer = producerBuilder.Build();
        }

        public async Task writePrice()
        {

            string? json = JsonCache.Get();
            if (json == null)
            {
                json = await GetTodaysPricesJsonAsync();
                JsonCache.Put(json);
            }

            PriceParser parser = new(json);

            DateTime now = DateTime.Now;
            TimeOnly time = new(now.Hour, 00);
            DateTime thisHour = DateOnly.FromDateTime(now).ToDateTime(time);

            double price = Convert.ToDouble(parser.GetWestPrice(thisHour));

            await this.producer.ProduceAsync(this.topicName, new Message<Null, double> { Value = price });
            return;
        }

        private static async Task<string> GetTodaysPricesJsonAsync()
        {

            HttpClient client = new();
            string payload = @"{  ""operationName"": ""Dataset"",  ""variables"": {},  ""query"": ""  query Dataset { elspotprices (order_by:{HourUTC:desc},limit:500,offset:0)  { HourUTC,HourDK,PriceArea,SpotPriceDKK,SpotPriceEUR } }""   }";

            var response = await client.PostAsync("https://data-api.energidataservice.dk/v1/graphql", new StringContent(payload, Encoding.UTF8, "application/json"));

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
