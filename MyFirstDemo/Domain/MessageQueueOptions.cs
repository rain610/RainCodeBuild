using System;

namespace Context
{
    public class MessageQueueOptions
    {
        public string ConsumerName { get; set; }
        public string NameServerAddresses { get; set; }
        public TopicsSettings Topics { get; set; }

        public class TopicsSettings
        {
            public string Flights { get; set; }
            public string Query { get; set; }
            public string Refresh { get; set; }
        }
    }
}
