using Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace StreamProcessing
{
    public class AggregatorAverage : IStreamProcessingOperation<double, IotMessage<double>?>
    {
        private readonly ILogger<AggregatorAverage> logger;
        private readonly Queue<double> values = new Queue<double>();

        public AggregatorAverage(ILogger<AggregatorAverage> logger)
        {
            this.logger = logger;
        }

        public IotMessage<double>? HandleMessage(IotMessage<double> message)
        {
            values.Enqueue(message.Message);
            logger.LogInformation("QueueLength: {length}", values.Count);

            if (values.Count < 10)
                return null;

            double sum = 0;
            foreach (var value in values)
                sum += value;

            var average = sum / values.Count;
            values.Clear();

            return new IotMessage<double>(average, DateTimeOffset.UtcNow, "aggregate");

        }
    }
}
