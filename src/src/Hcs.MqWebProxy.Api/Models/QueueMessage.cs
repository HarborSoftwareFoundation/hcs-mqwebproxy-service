﻿namespace Harbor.Hcs.MqWebProxy.Api.Models
{
    public class QueueMessage
    {
        public string Topic { get; set; }
        public string Message { get; set; }
    }
}