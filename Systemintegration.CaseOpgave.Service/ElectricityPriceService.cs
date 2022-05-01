using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systemintegration.CaseOpgave.Service.Contracts;
using Systemintegration.CaseOpgave.Service.DataPipelines;

namespace Systemintegration.CaseOpgave.Service
{
    public sealed class ElectricityPriceService : IElectricityPriceService
    {
        private const string topic = "current-price";
        private readonly ConsumerConfig consumerConfig;
        private readonly ProducerConfig producerConfig;

        public ElectricityPriceService(ConsumerConfig consumerConfig, ProducerConfig producerConfig)
        {
            this.producerConfig = producerConfig;
            this.consumerConfig = consumerConfig;
        }

        public double GetElectricityPrice()
        {

            var producer = new ProducerWrapper(this.producerConfig, topic);
            producer?.writePrice();

            var consumer = new ConsumerWrapper(consumerConfig, topic);
            double price = consumer.ReadPrice();

            return price;
        }
    }
}
