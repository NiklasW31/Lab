using Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamProcessing
{
    public class Filter : IStreamProcessingOperation<double, bool>
    {
        private readonly ILogger<Filter> logger;

        public Filter(ILogger<Filter> logger)
        {
            this.logger = logger;
        }

        public bool HandleMessage(IotMessage<double> message)
        {
            // TODO: Implement the logic to filter messages based on the instructions in Übung2.md.
            if(message.Message < 20)
            {
                logger.LogInformation($"Message accepted: {message.Message}");
                return true;
            }
            else
            {
                logger.LogInformation($"Message dismissed: {message.Message}");
                return false;
            }
            
            //throw new NotImplementedException();

        }
    }
}
