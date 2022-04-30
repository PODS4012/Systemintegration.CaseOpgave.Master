using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systemintegration.CaseOpgave.Service.DataPipelines
{
    public class ConsumerWrapper
    {
        private string topicName;
        private ConsumerConfig consumerConfig;
        private IConsumer<Null, double> consumer;

        public ConsumerWrapper(ConsumerConfig config, string topicName)
        {
            this.topicName = topicName;
            this.consumerConfig = config;
            var consumerBuilder = new ConsumerBuilder<Null, double>(this.consumerConfig);
            this.consumer = consumerBuilder.Build();
            this.consumer.Subscribe(topicName);
        }

        public double ReadPrice()
        {
            var consumeResult = this.consumer.Consume();

            double price = consumeResult.Message.Value;

            consumer.Close();

            return price;
        }
    }
}
